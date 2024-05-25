using Miratorg.TimeKeeper.BusinessLogic.Models.api;

namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public interface IApiService
{
    Task<ResponseDto> GetBoimetry(RequestDto requestDto);
    Task<ResponseDto> GetFiscal(RequestDto dto);
    Task<ResponseDto> GetManual(RequestDto dto);
}

public class ApiService : IApiService
{
    private readonly TimeKeeperDbContext _dbContext;
    private readonly ILogger<ApiService> _logger;

    public ApiService(TimeKeeperDbContext dbContext, ILogger<ApiService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ResponseDto> GetBoimetry(RequestDto requestDto)
    {
        try
        {
            return new ResponseDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");
        }

        return new ResponseDto();
    }

    private static int CalcTimeMinutes(DateTime begin, DateTime end)
    {
        var timeMinutes = (end - begin).TotalMinutes;

        if (timeMinutes > 180)
        {
            return (int)timeMinutes - 60;
        }

        return (int) timeMinutes;
    }

    public async Task<ResponseDto> GetFiscal(RequestDto requestDto)
    {
        try
        {
            var to = DateTime.Parse(requestDto.getTimesheets.to);
            var from = DateTime.Parse(requestDto.getTimesheets.from);

            var response = new ResponseDto()
            {
                timesheets = new List<Timesheet>()
            };
            
            if (Guid.TryParse(requestDto?.getTimesheets?.dep?.code, out var store1cId))
            {
                var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.StoreId1C == store1cId);

                if (store == null)
                {
                    throw new Exception($"Store not found {requestDto?.getTimesheets?.dep?.code}");
                }

                var employees = await _dbContext.Employees.Include(x => x.Plans).Where(x => x.StoreId == store.Id).ToListAsync();

                foreach (var employee in employees) 
                {
                    var times = Calc(employee.Plans, from, to, store1cId, employee.Guid1C.ToString());
                    response.timesheets.AddRange(times);
                }

                return response;
            }

            throw new Exception($"Employee not found by id: '{requestDto?.getTimesheets?.dep?.code}'");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");
        }

        return new ResponseDto();
    }

    private List<Timesheet> Calc(List<PlanEntity> plans, DateTime from, DateTime to, Guid storeId, string code1C)
    {
        List<Timesheet> timesheets = new List<Timesheet>();

        for (DateTime currentDate = from; currentDate <= to; currentDate = currentDate.AddDays(1))
        {
            List<PlanEntity> currentPlans = plans
                //.Where(x => x.StoreId == storeId)
                .Where(x => x.Begin >= currentDate && x.Begin <= currentDate.AddDays(1))
                .ToList();

            if (currentPlans.Count > 0)
            {
                int plan_minutesDay = 0;
                int plan_minutesNight = 0;

                int overwork_minutesDay = 0;
                int overwork_minutesNight = 0;

                Timesheet timesheet = new Timesheet()
                {
                    date = currentDate.ToString("yyyy-MM-dd"),
                    dep = new Dep() { code = storeId.ToString() },
                    nvalue = plan_minutesNight,
                    dvalue = plan_minutesDay,
                    employeeId = code1C,
                    dovertime = overwork_minutesDay,
                    novertime = overwork_minutesNight,
                    worktype = "Я",
                    worktime = new List<Worktime>()
                };

                bool isRemovePause = false; // признак что удалили час перерыва

                foreach (PlanEntity plan in currentPlans.Where(x => x.PlanType == PlanType.Plan).ToList())
                {
                    var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightHours(plan.Begin, plan.End);

                    if (isRemovePause == false && (dayMinutes + nightMinutes) > 180)
                    {
                        if (isRemovePause == false && dayMinutes > 180)
                        {
                            isRemovePause = true;
                            dayMinutes -= 60;
                        }

                        if (isRemovePause == false && nightMinutes > 180)
                        {
                            isRemovePause = true;
                            nightMinutes -= 60;
                        }
                    }

                    timesheet.worktime.Add(
                        new Worktime()
                        {
                            type = "regular",
                            dvalue = (int)dayMinutes,
                            nvalue = (int)nightMinutes,
                        });

                    plan_minutesDay += (int)dayMinutes;
                    plan_minutesNight += (int)nightMinutes;
                }

                timesheet.nvalue = plan_minutesNight;
                timesheet.dvalue = plan_minutesDay;

                foreach (PlanEntity plan in currentPlans.Where(x => x.PlanType == PlanType.Overwork).ToList())
                {
                    var (dayMinutes, nightMinutes) = TimeKeeperConverter.CalculateDayAndNightHours(plan.Begin, plan.End);

                    if (isRemovePause == false && (timesheet.nvalue + timesheet.dvalue + dayMinutes + nightMinutes) > 180)
                    {
                        if (isRemovePause == false && dayMinutes > 180)
                        {
                            isRemovePause = true;
                            dayMinutes -= 60;
                        }

                        if (isRemovePause == false && nightMinutes > 180)
                        {
                            isRemovePause = true;
                            nightMinutes -= 60;
                        }
                    }

                    timesheet.worktime.Add(
                        new Worktime()
                        {
                            dvalue = (int)dayMinutes,
                            nvalue = (int)nightMinutes,
                            type = "regular" //ToDo - Добавить корректный тип работы
                        });

                    overwork_minutesDay += (int)dayMinutes;
                    overwork_minutesNight += (int)nightMinutes;
                }

                timesheet.dovertime = overwork_minutesDay;
                timesheet.novertime = overwork_minutesNight;

                timesheets.Add(timesheet);
            }
            else
            {
                Timesheet timesheet = new Timesheet()
                {
                    date = currentDate.ToString("yyyy-MM-dd"),
                    dep = new Dep() { code = storeId.ToString() },
                    nvalue = 0,
                    dvalue = 0,

                    employeeId = code1C,
                    
                    dovertime = 0,
                    novertime = 0,
                    worktype = "я",
                    worktime = new List<Worktime>()
                };

                timesheets.Add(timesheet);
            }
        }

        return timesheets;
    }

    public async Task<ResponseDto> GetManual(RequestDto requestDto)
    {
        try
        {
            return new ResponseDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");
        }

        return new ResponseDto();
    }
}
