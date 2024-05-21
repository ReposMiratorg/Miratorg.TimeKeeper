namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public interface IPlanService
{
    //public Task<List<PlanEntity>> GetPlan(DateTime beginDate, DateTime endDate);
    //public Task<List<EmployeeModel>> GetPlanModel(DateTime beginDate, DateTime endDate);
    public Task Create(Guid employeeId, PlanType planType, DateTime beginWork, DateTime endWork, Guid storeId, Guid? typeOverwork, Guid? customOverwork);
    public Task Remove(Guid id);
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

        await  dbContext.SaveChangesAsync();
    }

    public async Task<List<PlanEntity>> GetPlan(DateTime startDate, DateTime endDate)
    {
        ValidateDate(startDate);
        ValidateDate(endDate);

        using var dbContext = await _dbContextFactory.Create();
        var plans = await dbContext.Plans
            .Where(x => (x.Begin >= startDate && x.Begin <= endDate) || (x.End >= startDate && x.End <= endDate))
            .ToListAsync();
        
        return plans;
    }

    public async Task Remove(Guid id)
    {
        using var dbContext = await _dbContextFactory.Create();
        var planEntity = await dbContext.Plans.FirstOrDefaultAsync(x => x.Id == id);
        dbContext.Plans.Remove(planEntity);
        await dbContext.SaveChangesAsync();
    }

    private static void ValidateDate(DateTime dateTime)
    {
        var date = dateTime.Date;

        if (dateTime != date)
        {
            throw new InvalidParameterPlanServiceException(dateTime.ToString());
        }
    }

    private static void ValidateDates(DateTime begin, DateTime end)
    {
        if (begin >= end)
        {
            throw new InvalidParameterPlanServiceException($"Incorrect interval {begin} - {begin}");
        }
    }

    public async Task<List<EmployeeModel>> GetPlanModel(DateTime startDate, DateTime endDate)
    {
        ValidateDate(startDate);
        ValidateDate(endDate);

        using var dbContext = await _dbContextFactory.Create();
        var plans = await dbContext.Plans.Where(x => x.Begin >= startDate && x.End <= endDate).ToListAsync();

        var employees = await dbContext.Employees.Include(x => x.Store).Include(x => x.Schedule).ThenInclude(x => x.Dates).ToListAsync();

        var models = new List<EmployeeModel>();

        var groups = plans
            .GroupBy(x => x.EmployeeId)
            .Select(x => new { key = x.Key, Data = x.ToList()});

        foreach (var item in employees)
        {
            var model = new EmployeeModel
            {
                Id = item.Id,
                StoreId = item.StoreId
            };

            var plan = groups.FirstOrDefault(x => x.key == item.Id);

            if (plan != null)
            {
                foreach (var d in plan.Data)
                {
                    model.Plans.Add(new PlanDetailModel()
                    {
                        Id = d.Id,
                        Begin = d.Begin,
                        End = d.End,
                        PlanType = d.PlanType,
                        StoreId = d.StoreId
                    });
                }
            }

            var employee = SyncEmployeeService.Employees.FirstOrDefault(x => x.Id == item.Id);
            if (employee?.WorkDates != null)
            {
                var dates = employee?.WorkDates
                    .Where(x => x.Begin >= startDate && x.Begin <= endDate.AddDays(1))
                    .ToList();

                foreach (var date in dates)
                {
                    model.WorkDates.Add(new Schedule1CPlanModel()
                    {
                         Begin = date.Begin,
                         End = date.End,
                    });
                }

                var scudInfos = employee?.ScudInfos?.Where(x => x.Begin >= startDate && x.End <= endDate)
                    .ToList();

                foreach (var scudFact in scudInfos)
                {
                    model.ScudInfos.Add(new ScudInfoModel()
                    {
                        Begin = scudFact.Begin,
                        End = scudFact.End
                    });
                }
            }

            models.Add(model);
        }

        return models;
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

