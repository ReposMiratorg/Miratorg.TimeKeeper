namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public interface IPlanService
{
    public Task<bool> CheckPlan(Guid employeeId, DateTime beginWork, DateTime endWork);
    public Task Create(Guid employeeId, PlanType planType, DateTime beginWork, DateTime endWork, Guid storeId, Guid? typeOverwork, Guid? customOverwork);
    public Task Remove(Guid id);
    public Task RemoveScudManual(Guid id);
    public Task CreateManualScud(Guid employeeId, DateTime beginWork, DateTime endWork);
}

public class PlanService : IPlanService
{
    private readonly ITimeKeeperDbContextFactory _dbContextFactory;
    private readonly ILogger<PlanService> _logger;

    public PlanService(ITimeKeeperDbContextFactory dbContextFactory, ILogger<PlanService> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task Create(Guid employeeId, PlanType planType, DateTime begin, DateTime end, Guid storeId, Guid? typeOverwork, Guid? customOverwork)
    {
        ValidateDates(begin, end);

        if (storeId == Guid.Empty)
        {
            throw new Exception("StoreId == Guid.Empty");
        }

        using var dbContext = await _dbContextFactory.Create();

        var existPlans = await dbContext.Plans.Where(x => x.EmployeeId == employeeId && x.PlanType == planType).ToListAsync();

        foreach (var item in existPlans)
        {
            // Hire check conditions
            if (false)
            {
                throw new PlanServiceException("test");
            }
        }

        if (begin.Date != end.Date)
        {
            var plan0 = new PlanEntity()
            {
                EmployeeId = employeeId,
                Begin = begin,
                End = begin.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                PlanType = planType,
                StoreId = storeId,
                TypeOverWorkId = typeOverwork,
                CustomTypeWorkId = customOverwork
            };

            var plan1 = new PlanEntity()
            {
                EmployeeId = employeeId,
                Begin = end.Date,
                End = end,
                PlanType = planType,
                StoreId = storeId,
                TypeOverWorkId = typeOverwork,
                CustomTypeWorkId = customOverwork
            };

            dbContext.Plans.Add(plan0);
            dbContext.Plans.Add(plan1);
        }
        else
        {
            var plan = new PlanEntity()
            {
                EmployeeId = employeeId,
                Begin = begin,
                End = end,
                PlanType = planType,
                StoreId = storeId,
                TypeOverWorkId = typeOverwork,
                CustomTypeWorkId = customOverwork
            };

            dbContext.Plans.Add(plan);
        }

        await  dbContext.SaveChangesAsync();
    }

    public async Task RemoveScudManual(Guid id)
    {
        try
        {
            using var dbContext = await _dbContextFactory.Create();
            var  manualScudEntity = await dbContext.ManualScuds.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.ManualScuds.Remove(manualScudEntity);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {

        }
    }

    public async Task Remove(Guid id)
    {
        try
        {
            using var dbContext = await _dbContextFactory.Create();
            var planEntity = await dbContext.Plans.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.Plans.Remove(planEntity);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {

        }
    }

    private static void ValidateDates(DateTime begin, DateTime end)
    {
        if (begin >= end)
        {
            throw new InvalidParameterPlanServiceException($"Incorrect interval {begin} - {begin}");
        }
    }

    public async Task CreateManualScud(Guid employeeId, DateTime begin, DateTime end)
    {
        using var dbContext = await _dbContextFactory.Create();

        if (begin.Date != end.Date)
        {
            ManualScudEntity manualScudEntity0 = new ManualScudEntity()
            {
                CreateAt = DateTime.Now,
                EmployeeId = employeeId,
                Input = begin,
                Output = begin.AddHours(23).AddMinutes(59).AddSeconds(59),
                UserAutorName = "n/d"
            };

            ManualScudEntity manualScudEntity1 = new ManualScudEntity()
            {
                CreateAt = DateTime.Now,
                EmployeeId = employeeId,
                Input = end.Date,
                Output = end,
                UserAutorName = "n/d"
            };

            dbContext.ManualScuds.Add(manualScudEntity0);
            dbContext.ManualScuds.Add(manualScudEntity1);
        }
        else
        {
            ManualScudEntity manualScudEntity = new ManualScudEntity()
            {
                CreateAt = DateTime.Now,
                EmployeeId = employeeId,
                Input = begin,
                Output = end,
                UserAutorName = "n/d"
            };

            dbContext.ManualScuds.Add(manualScudEntity);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> CheckPlan(Guid employeeId, DateTime begin, DateTime end)
    {
        try
        {
            using var dbContext = await _dbContextFactory.Create();

            var result = dbContext.Plans.Any(x => x.EmployeeId == employeeId && begin < x.End && end > x.Begin);

            if (result == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }
}

public class PlanServiceException : MiratorgBaseException
{
    public const string EXISIT_RECORD = "Существует план на это время";

    public PlanServiceException(string message, params object?[] args0)
        : base(message, args0)
    {
    }

    public PlanServiceException(Exception exception, string message, params object?[] args0)
        : base(exception, message, args0)
    {
    }
}

public class InvalidParameterPlanServiceException : PlanServiceException
{
    public const string INVALID_DATE = "Дата не является корректной";

    public InvalidParameterPlanServiceException(string message, params object?[] args0)
       : base(message, args0)
    {
    }

    public InvalidParameterPlanServiceException(Exception exception, string message, params object?[] args0)
        : base(exception, message, args0)
    {
    }
}

