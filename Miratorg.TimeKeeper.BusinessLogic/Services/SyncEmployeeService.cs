using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public class SyncEmployeeService : IHostedService
{
    public static List<EmployeeEntity> Employees { get; private set; } = new List<EmployeeEntity>();

    private readonly IStuffControlDbService _stuffControlService;
    private readonly ITimeKeeperDbContextFactory _timeKeeperDbContextFactory;
    private readonly IStaffControlDbContextFactory _staffControlDbContextFactory;
    private readonly ILogger<SyncEmployeeService> _logger;

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

    private async Task Process()
    {
        try
        {
            var staffDbContext = await _staffControlDbContextFactory.Create();
            using var dbContext = await _timeKeeperDbContextFactory.Create();

            Employees.Clear();
            Employees = await dbContext.Employees.Include(x => x.Schedule).ThenInclude(x => x.Dates).ToListAsync();

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

                if (store == null)
                {
                    store = new StoreEntity()
                    {
                        Name = employee.RoutineDivision
                    };

                    dbContext.Stores.Add(store);
                    await dbContext.SaveChangesAsync();
                }

                if (currentEmployee == null)
                {
                    currentEmployee = new EmployeeEntity()
                    {
                        CodeNav = employee.Code,
                        Name = $"{employee.LastName} {employee.FirstName} {employee.MiddleName}",
                        StoreId = store.Id,
                        Position = position?.Description ?? "n/d"
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

                    await dbContext.SaveChangesAsync();
                }

                await UpdateSchedule(currentEmployee.Id, employee.Code);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error process");
        }
    }

    private async Task UpdateSchedule(Guid userId, string codeNav)
    {
        try
        {
            var (start, end) = GetFirstAndLastDayOfMonth(DateTime.Now);
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
            _logger.LogError($"'{nameof(UpdateSchedule)}': {ex.Message}", ex);
        }
    }

    public static (DateTime FirstDayOfMonth, DateTime LastDayOfMonth) GetFirstAndLastDayOfMonth(DateTime date)
    {
        DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
        DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        return (firstDayOfMonth, lastDayOfMonth);
    }
}