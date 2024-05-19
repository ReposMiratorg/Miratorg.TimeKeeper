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
                    foreach (var plan in employee.Plans)
                    {
                        if (plan.StoreId == store.Id && from.Date >= plan.Begin.Date && plan.End.Date <= to.Date)
                        {
                            var timesheet = new Timesheet()
                            {
                                employeeId = employee.Guid1C.ToString(),
                                date = plan.Begin.ToString("yyyy-MM-dd"),
                                dep = new Dep() { code = store1cId.ToString() },
                                nvalue = 0,
                                dvalue = CalcTimeMinutes(plan.Begin, plan.End),
                                dovertime = 0,
                                novertime = 0,
                                worktime = new List<Worktime>()
                            };

                            timesheet.worktime.Add(new Worktime()
                            {
                                dvalue = CalcTimeMinutes(plan.Begin, plan.End),
                                nvalue = 0,
                                type = "regular"
                            });

                            response.timesheets.Add(timesheet);
                        }
                    }
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
