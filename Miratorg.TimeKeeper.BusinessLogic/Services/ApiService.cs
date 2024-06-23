using Miratorg.TimeKeeper.BusinessLogic.Models.api;

namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public class ApiService : IApiService
{
    private readonly TimeKeeperDbContext _dbContext;
    private readonly ILogger<ApiService> _logger;

    public ApiService(TimeKeeperDbContext dbContext, ILogger<ApiService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ResponseDto> GetFacts(RequestDto requestDto)
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

                var employees = await _dbContext.Employees.Where(x => x.StoreId == store.Id).ToListAsync();

                foreach (var employee in employees)
                {
                    var entity = await SyncEmployeeService.GetUserById(employee.Id);
                    
                    List<SigurEventModel> sigurEvents = new List<SigurEventModel>();

                    foreach (var item in entity.SigurInfos)
                    {
                        sigurEvents.Add(new SigurEventModel() { CodeNav = item.CodeNav, EventTime = item.EventTime });
                    }

                    var model = TimeKeeperConverter.ConvertV4(entity, sigurEvents);

                    var timeSchist = ConverScudToTimesheetBiomentry(model, from, to, store1cId, employee.Guid1C.ToString());

                    response.timesheets.AddRange(timeSchist);
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

    public static IEnumerable<Timesheet> ConverScudToTimesheetBiomentry(EmployeeModel model, DateTime from, DateTime to, Guid storeId, string code1C)
    {
        var timesheets = new List<Timesheet>();

        // формируем данные на каждый день
        for (DateTime currentDate = from; currentDate <= to; currentDate = currentDate.AddDays(1))
        {
            var facts = model.ExportFactTimes
                .Where(x => x.Begin.Date.Date == currentDate)
                .ToList();

            if (facts.Count > 0)
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
                    worktype = "Я",
                    worktime = new List<Worktime>()
                };

                foreach (var fact in facts)
                {
                    Worktime worktime = new()
                    {
                        type = fact.WorkTime,
                        dvalue = fact.DayMinutes,
                        nvalue = fact.NightMinutes
                    };

                    timesheet.worktime.Add(worktime);

                    if (fact.WorkTime == "regular")
                    {
                        timesheet.dvalue += fact.DayMinutes;
                        timesheet.nvalue += fact.NightMinutes;
                    }
                    else
                    {
                        timesheet.dovertime += fact.DayMinutes;
                        timesheet.novertime += fact.NightMinutes;
                    }
                }

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
                    worktype = "Я",
                    worktime = new List<Worktime>()
                };

                timesheets.Add(timesheet);
            }
        }

        return timesheets;
    }

    public static List<Timesheet> ConverPlanToTimesheetFiscal(EmployeeModel model, DateTime from, DateTime to, Guid storeId, string code1C)
    {
        var timesheets = new List<Timesheet>();

        for (DateTime currentDate = from; currentDate <= to; currentDate = currentDate.AddDays(1))
        {
            var plans = model.ExportPlanTimes
                .Where(x => x.Begin.Date.Date == currentDate)
                .ToList();

            if (plans.Count > 0)
            {
                int plan_minutesDay = 0;
                int plan_minutesNight = 0;

                int overwork_minutesDay = 0;
                int overwork_minutesNight = 0;

                Timesheet timesheet = new Timesheet()
                {
                    date = currentDate.ToString("yyyy-MM-dd"),
                    dep = new Dep() { code = storeId.ToString() },
                    nvalue = 0,
                    dvalue = 0,
                    employeeId = code1C,
                    dovertime = 0,
                    novertime = 0,
                    worktype = "Я",
                    worktime = new List<Worktime>()
                };

                foreach (var fact in plans)
                {
                    Worktime worktime = new()
                    {
                        type = fact.WorkTime,
                        dvalue = fact.DayMinutes,
                        nvalue = fact.NightMinutes
                    };

                    timesheet.worktime.Add(worktime);

                    if (fact.WorkTime == "regular")
                    {
                        timesheet.dvalue += fact.DayMinutes;
                        timesheet.nvalue += fact.NightMinutes;
                    }
                    else
                    {
                        timesheet.dovertime += fact.DayMinutes;
                        timesheet.novertime += fact.NightMinutes;
                    }
                }

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
                    worktype = "Я",
                    worktime = new List<Worktime>()
                };

                timesheets.Add(timesheet);
            }
        }

        return timesheets;
    }

    public async Task<ResponseDto> GetPlans(RequestDto requestDto)
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

                var employees = await _dbContext.Employees.Where(x => x.StoreId == store.Id).ToListAsync();

                foreach (var employee in employees) 
                {
                    var entity = await  SyncEmployeeService.GetUserById(employee.Id);
                    List<SigurEventModel> sigurEvents = new List<SigurEventModel>();

                    foreach (var item in entity.SigurInfos)
                    {
                        sigurEvents.Add(new SigurEventModel() { CodeNav = item.CodeNav, EventTime = item.EventTime });
                    }

                    var model = TimeKeeperConverter.ConvertV4(entity, sigurEvents);

                    var timeSchist = ConverPlanToTimesheetFiscal(model, from, to, store1cId, employee.Guid1C.ToString());

                    response.timesheets.AddRange(timeSchist);
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
