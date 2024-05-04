using Microsoft.Extensions.Logging;
using Miratorg.TimeKeeper.DataAccess.Entities;

namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public interface IPlanService
{
    public Task<List<PlanEntity>> GetPlan(DateTime startDate, DateTime endDate);
    public Task Create(Guid employeeId, DateTime dateKey, PlanType planType, DateTime startWork, DateTime endWork);
    public Task Remove(Guid employeeId, DateTime dateKey);
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

    public async Task Create(Guid employeeId, DateTime dateKey, PlanType planType, DateTime startWork, DateTime endWork)
    {
        ValidateDate(dateKey);
        using var dbContext = await _dbContextFactory.Create();

        var existPlans = await dbContext.Plans.Where(x => x.EmployeeId == employeeId && x.DateKey == dateKey && x.PlanType == planType).ToListAsync();

        foreach (var item in existPlans)
        {
            // Hire check conditions
            if (false)
            {
                throw new PlanServiceException("test");
            }
        }

        dbContext.Plans.Add(new PlanEntity()
        {
            EmployeeId = employeeId,
             DateKey = dateKey,

        });

        await  dbContext.SaveChangesAsync();
    }

    public async Task<List<PlanEntity>> GetPlan(DateTime startDate, DateTime endDate)
    {
        ValidateDate(startDate);
        ValidateDate(endDate);

        using var dbContext = await _dbContextFactory.Create();
        var plans = await dbContext.Plans.Where(x => x.DateKey >= startDate && x.DateKey <= endDate).ToListAsync();
        
        return plans;
    }

    public async Task Remove(Guid employeeId, DateTime dateKey)
    {
        ValidateDate(dateKey);
        using var dbContext = await _dbContextFactory.Create();
    }

    private static void ValidateDate(DateTime dateTime)
    {
        var date = dateTime.Date;

        if (dateTime != date)
        {
            throw new InvalidParameterPlanServiceException(dateTime.ToString());
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

