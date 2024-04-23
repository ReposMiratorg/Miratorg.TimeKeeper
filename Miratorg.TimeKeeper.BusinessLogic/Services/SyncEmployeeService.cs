using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Miratorg.DataService.Contexts;
using Miratorg.DataService.Interfaces;
using Miratorg.TimeKeeper.DataAccess.Contexts;
using Miratorg.TimeKeeper.DataAccess.Entities;

namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public class SyncEmployeeService : IHostedService
{
    private readonly IStuffControlDbService _stuffControlService;
    private readonly ITimeKeeperDbContextFactory _timeKeeperDbContextFactory;
    private readonly IStuffControlDbContextFactory _stuffControlDbContextFactory;
    private readonly ILogger<SyncEmployeeService> _logger;

    private bool isWork { get; set; }
    private readonly TimeSpan pause = TimeSpan.FromDays(1);

    public SyncEmployeeService(IStuffControlDbService stuffControlService, IStuffControlDbContextFactory stuffControlDbContextFactory, ITimeKeeperDbContextFactory timeKeeperDbContextFactory, ILogger<SyncEmployeeService> logger)
    {
        _stuffControlService = stuffControlService;
        _timeKeeperDbContextFactory = timeKeeperDbContextFactory;
        _stuffControlDbContextFactory = stuffControlDbContextFactory;
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
                    _logger.LogError(ex, $"Error ''");
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
            var employees = await _stuffControlService.GetAllEmployees();
            if (employees == null)
            {
                return;
            }

            var dbContext = await _timeKeeperDbContextFactory.Create();

            foreach (var employee in employees)
            {
                _logger.LogInformation($"Process: '{employee.Code}'");

                var currentEmployee = dbContext.Employees.FirstOrDefault(x => x.CodeNav == employee.Code);
                if (currentEmployee == null)
                {
                    dbContext.Employees.Add(new EmployeeEntity()
                    {
                        CodeNav = employee.Code,
                        Name = $"{employee.LastName} {employee.FirstName} {employee.MiddleName}",
                        Division = employee.Division
                    });

                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    currentEmployee.Name = $"{employee.LastName} {employee.FirstName} {employee.MiddleName}";
                    currentEmployee.CodeNav = employee.Code;
                    currentEmployee.Division = employee.Division;

                    var boss = dbContext.Employees.FirstOrDefault(x => x.CodeNav == employee.CodeBoss);
                    if (boss != null)
                    {
                        currentEmployee.BossId = boss.Id;
                    }
                    else
                    {
                        currentEmployee.BossId = null;
                    }

                    await UpdateSchedule(currentEmployee.Id, employee.Code);

                    await dbContext.SaveChangesAsync();
                }
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
            var stuffContext = await _stuffControlDbContextFactory.Create();

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
                _logger.LogInformation($"Create schedule '{schedule.Name}'");
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