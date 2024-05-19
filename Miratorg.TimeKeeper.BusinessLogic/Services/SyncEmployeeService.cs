﻿using System;

namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public class SyncEmployeeService : IHostedService
{
    public static List<EmployeeModel> Employees { get; private set; } = new List<EmployeeModel>();

    private readonly IStuffControlDbService _stuffControlService;
    private static ITimeKeeperDbContextFactory _timeKeeperDbContextFactory;
    private readonly IStaffControlDbContextFactory _staffControlDbContextFactory;
    private static ILogger<SyncEmployeeService> _logger;

    private bool isWork { get; set; }
    private readonly TimeSpan pause = TimeSpan.FromDays(1);

    public SyncEmployeeService(IStuffControlDbService stuffControlService,
        IStaffControlDbContextFactory staffControlDbContextFactory,
        ITimeKeeperDbContextFactory timeKeeperDbContextFactory,
        ILogger<SyncEmployeeService> logger)
    {
        _stuffControlService = stuffControlService;
        _timeKeeperDbContextFactory = timeKeeperDbContextFactory;
        _staffControlDbContextFactory = staffControlDbContextFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (isWork == true)
        {
            throw new Exception($"Service '{nameof(SyncEmployeeService)}' already started");
        }

        isWork = true;

        _ = Task.Run(async () =>
        {
            while (isWork == true)
            {
                try
                {
                    await Process();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error '{nameof(SyncEmployeeService)}::{nameof(Process)}'");
                }

                await Task.Delay(pause);
            }
        });
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        isWork = false;
    }

    private async Task UpdateUsers()
    {
        using var dbContext = await _timeKeeperDbContextFactory.Create();

        var employees = await dbContext.Employees
            .Include(x => x.Schedule).ThenInclude(x => x.Dates)
            .Include(x => x.ScudInfos)
            .Include(x => x.Plans).ThenInclude(x => x.TypeOverWork)
            .Include(x => x.Absences)
            .OrderBy(x => x.Name)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();

        List<EmployeeModel> models = new List<EmployeeModel>();

        foreach (var employee in employees)
        {
            var model = TimeKeeperConverter.Convert(employee);
            models.Add(model);
        }

        Employees.Clear();
        Employees = models;
    }

    private async Task SyncAbsence(Guid userId, DateTime begin, DateTime end)
    {
        try
        {
            using var staffDbContext = await _staffControlDbContextFactory.Create();
            using var dbContext = await _timeKeeperDbContextFactory.Create();

            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == userId);
            if (employee == null)
            {
                throw new Exception("employee not found");
            }

            var absences = await staffDbContext.StaffAbsences
                .Where(x => x.Code == employee.CodeNav && x.RepDate >= begin && x.RepDate <= end)
                .AsNoTracking()
                .ToListAsync();

            var absencesExist = await dbContext.Absences
                .Where(x => x.Id == employee.Id && x.RepDate >= begin && x.RepDate < end)
                .AsNoTracking()
                .ToListAsync();

            if (absencesExist.Count > 0)
            {
                dbContext.Absences.RemoveRange(absencesExist);
                await dbContext.SaveChangesAsync();
            }

            if (absences.Count > 0)
            {
                foreach (var absence in absences)
                {
                    dbContext.Absences.Add(new AbsenceEntity()
                    {
                        EmployeeId = employee.Id,
                        AbsenceCode = absence.CodeType,
                        AbsenceDescription = absence.Description1,
                        RepDate = absence.RepDate
                    });
                }

                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sync absence: {ex.Message}");
        }
    }

    public static async Task UpdateUser(Guid userId)
    {
        try
        {
            using var dbContext = await _timeKeeperDbContextFactory.Create();

            var employee = await dbContext.Employees
                .Include(x => x.Schedule).ThenInclude(x => x.Dates)
                .Include(x => x.ScudInfos)
                .Include(x => x.Plans)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.Id == userId);

            var existUser = Employees.FirstOrDefault(x => x.Id == userId);

            if (existUser != null)
            {
                for (var i = 0; i < Employees.Count; i++)
                {
                    if(Employees[i].Id == userId)
                    {
                        Employees[i] = TimeKeeperConverter.Convert(employee);
                    }
                }
            }
            else
            {
                Employees.Add(TimeKeeperConverter.Convert(employee));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении");
        }
    }

    private async Task Process()
    {
        try
        {
            var staffDbContext = await _staffControlDbContextFactory.Create();
            using var dbContext = await _timeKeeperDbContextFactory.Create();

            await UpdateUsers();

            var employees = await staffDbContext.Staff
                .Where(x => x.LegalEntity == "ООО \"ПродМир\"" || x.LegalEntity == "ООО «Стейк и Бургер»")
                .Where(x => x.DismissalDate == null)
                .ToListAsync();

            var divisions = await staffDbContext.StaffDivisions.ToListAsync();
            var positions = await staffDbContext.StaffPositions.ToListAsync();

            if (employees == null)
            {
                return;
            }

            foreach (var employee in employees)
            {
                _logger.LogInformation($"Process: '{employee.Code}'");

                var currentEmployee = dbContext.Employees.FirstOrDefault(x => x.CodeNav == employee.Code);
                var position = positions.FirstOrDefault(x => x.Code.ToLower() == employee.CodePosition);

                var store = dbContext.Stores.FirstOrDefault(x => x.Name == employee.RoutineDivision);

                Guid store1C = Guid.Parse(employee.CodeRoutineDivision);

                if (store == null)
                {
                    store = new StoreEntity()
                    {
                        Name = employee.RoutineDivision,
                        StoreId1C = store1C
                    };

                    dbContext.Stores.Add(store);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    store.Name = employee.RoutineDivision;
                    store.StoreId1C = store1C;
                    await dbContext.SaveChangesAsync();
                }

                if (currentEmployee == null)
                {
                    currentEmployee = new EmployeeEntity()
                    {
                        CodeNav = employee.Code,
                        Name = $"{employee.LastName} {employee.FirstName} {employee.MiddleName}",
                        StoreId = store.Id,
                        Position = position?.Description ?? "n/d",
                        Guid1C = employee.Guid ?? Guid.Empty
                    };

                    dbContext.Employees.Add(currentEmployee);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    currentEmployee.Name = $"{employee.LastName} {employee.FirstName} {employee.MiddleName}";
                    currentEmployee.CodeNav = employee.Code;
                    currentEmployee.StoreId = store.Id;
                    currentEmployee.Position = position?.Description ?? "n/d";
                    currentEmployee.Guid1C = employee.Guid ?? Guid.Empty;

                    await dbContext.SaveChangesAsync();
                }

                var (start, end) = GetFirstAndLastDayOfMonth(DateTime.Now);

                var tempDate = start.Date;
                for (int i = 0; tempDate <= end; i++)
                {
                    // обрабатываем данные из проховов СКУД
                    await UpdateScudData(currentEmployee.Id, employee.Code, tempDate, tempDate.AddDays(1));

                    tempDate = tempDate.AddDays(1);
                }

                    // Обновляем причины отсутствия
                await SyncAbsence(currentEmployee.Id, end, tempDate);

                // обрабатываем данные из плана проходов 1С
                await UpdateSchedule(currentEmployee.Id, employee.Code, start, end);

                // обновляем модель после обновления в БД
                await UpdateUser(currentEmployee.Id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error process");
        }
    }

    private async Task UpdateScudData(Guid userId, string codeNav, DateTime start, DateTime end)
    {
        try
        {
            var dbContext = await _timeKeeperDbContextFactory.Create();
            var stuffContext = await _staffControlDbContextFactory.Create();

            var scudInfo = dbContext.ScudInfos.FirstOrDefault(x => x.EmployeeId == userId && x.Input >= start && x.Output < end);

            if (scudInfo != null)
            {
                return;
            }

            var scudStaff = await stuffContext.SkudStaffs.FirstOrDefaultAsync(x => x.Code != null && x.Code.ToLower() == codeNav.ToLower());
            if (scudStaff == null)
            {
                //_logger.LogInformation($"ScudStaff :'{codeNav}' - not found.");
                return;
            }

            var datas = await stuffContext.SkudDatas
                .Where(x => x.IdStaff == scudStaff.Id && start <= x.RepDate && end > x.RepDate)
                .ToListAsync();

            if (datas.Count == 0)
            {
                return;
            }

            var input = datas.Where(x => x.TypePass == 1).MinBy(x => x.RepDate)?.RepDate;
            var output = datas.Where(x => x.TypePass == 2).MaxBy(x => x.RepDate)?.RepDate;

            if (input != null && output != null && input < output)
            {
                dbContext.ScudInfos.Add(new ScudInfo()
                {
                    EmployeeId = userId,
                    Input = (DateTime)input,
                    Output = (DateTime)output
                });

                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"'{nameof(UpdateScudData)}': {ex.Message}");
        }
    }

    private async Task UpdateSchedule(Guid userId, string codeNav, DateTime start, DateTime end)
    {
        try
        {
            var dbContext = await _timeKeeperDbContextFactory.Create();
            var stuffContext = await _staffControlDbContextFactory.Create();

            var employee = dbContext.Employees.FirstOrDefault(x => x.Id == userId);
            if (employee == null)
            {
                _logger.LogError($"NotFoundUser:{userId}");
                return;
            }

            //ToDo - добавляем дни
            var dates = await _stuffControlService.GetScheduleDays(codeNav, start, end);

            if (dates.Count == 0)
            {
                _logger.LogInformation($"not dates for '{codeNav}'");
                return;
            }

            //Проверяем есть ли назначенное расписание

            var scheduleId = dates.Max(x => x.WorkScheduleID);
            var scheduleName = stuffContext.IASWorkSchedules.FirstOrDefault(x => x.WorkScheduleID == scheduleId)?.WorkScheduleName;

            if (scheduleName == null || scheduleId == null)
            {
                scheduleId = 0;
                scheduleName = "N/D";
            }

            var schedule = dbContext.Schedules.Include(x => x.Dates).FirstOrDefault(x => x.Name == scheduleName && x.Code == scheduleId);
            if (schedule == null)
            {
                _logger.LogInformation($"Create schedule '{scheduleName}'");
                dbContext.Schedules.Add(new ScheduleEntity() { Code = (int)scheduleId, Name = scheduleName });
                dbContext.SaveChanges();

                schedule = dbContext.Schedules.FirstOrDefault(x => x.Name == scheduleName && x.Code == scheduleId);
            }

            // Добавляем расписание пользователю
            employee.ScheduleId = schedule.Id;
            dbContext.SaveChanges();

            foreach (var date in dates)
            {
                if (date.IsWorkDay == null)
                {
                    continue;
                }

                var record = schedule.Dates.FirstOrDefault(x => x.TimeBegin == date.TimeBegin && x.TimeEnd == date.TimeEnd);
                if (record == null)
                {
                    dbContext.ScheduleDates.Add(new ScheduleDateEntity() { TimeBegin = date.TimeBegin, TimeEnd = date.TimeEnd, ScheduleId = schedule.Id });
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"'{nameof(UpdateSchedule)}': {ex.Message}");
        }
    }

    public static (DateTime FirstDayOfMonth, DateTime LastDayOfMonth) GetFirstAndLastDayOfMonth(DateTime date)
    {
        DateTime firstDayOfMonth = date.AddMonths(-1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        return (firstDayOfMonth, lastDayOfMonth);
    }
}