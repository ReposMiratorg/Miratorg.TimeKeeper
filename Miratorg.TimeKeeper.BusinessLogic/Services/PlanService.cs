using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Miratorg.TimeKeeper.DataAccess.Entities;
using System.Numerics;

namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public interface IPlanService
{
    public Task<bool> CheckPlan(Guid employeeId, DateTime beginWork, DateTime endWork);
    public Task Create(Guid employeeId, PlanType planType, DateTime beginWork, DateTime endWork, Guid storeId, Guid? typeOverwork, Guid? customOverwork, string autor);
    public Task Remove(Guid id, string autor);
    public Task RemoveScudManual(Guid id, string autor);
    public Task CreateManualScud(Guid employeeId, DateTime beginWork, DateTime endWork, string autor);
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

    public async Task Create(Guid employeeId, PlanType planType, DateTime begin, DateTime end, Guid storeId, Guid? typeOverwork, Guid? customOverwork, string autor)
    {
        ValidateDates(begin, end);

        if (string.IsNullOrEmpty(autor))
        {
            autor = "n/d";
        }

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

            dbContext.LogPlans.Add(CreatePlanLog(plan0, autor, TypeLogEvent.Create));
            dbContext.LogPlans.Add(CreatePlanLog(plan1, autor, TypeLogEvent.Create));
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
            dbContext.LogPlans.Add(CreatePlanLog(plan, autor, TypeLogEvent.Create));
        }

        await  dbContext.SaveChangesAsync();
    }

    public async Task RemoveScudManual(Guid id, string autor)
    {
        try
        {
            if (string.IsNullOrEmpty(autor))
            {
                autor = "n/d";
            }

            using var dbContext = await _dbContextFactory.Create();
            var  manualScudEntity = await dbContext.ManualScuds.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.ManualScuds.Remove(manualScudEntity);
            dbContext.LogManualScuds.Add(CreateScudLog(manualScudEntity, autor, TypeLogEvent.Delete));
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {

        }
    }

    public async Task Remove(Guid id, string autor)
    {
        try
        {
            if (string.IsNullOrEmpty(autor))
            {
                autor = "n/d";
            }

            using var dbContext = await _dbContextFactory.Create();
            var planEntity = await dbContext.Plans.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.Plans.Remove(planEntity);
            await dbContext.SaveChangesAsync();
            dbContext.LogPlans.Add(CreatePlanLog(planEntity, autor, TypeLogEvent.Delete));
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

    public async Task CreateManualScud(Guid employeeId, DateTime begin, DateTime end, string autor)
    {
        using var dbContext = await _dbContextFactory.Create();

        if (string.IsNullOrEmpty(autor))
        {
            autor = "n/d";
        }

        if (begin.Date != end.Date)
        {
            ManualScudEntity manualScudEntity0 = new ManualScudEntity()
            {
                EmployeeId = employeeId,
                Input = begin,
                Output = begin.AddHours(23).AddMinutes(59).AddSeconds(59)
            };

            ManualScudEntity manualScudEntity1 = new ManualScudEntity()
            {
                EmployeeId = employeeId,
                Input = end.Date,
                Output = end
            };

            dbContext.ManualScuds.Add(manualScudEntity0);
            dbContext.ManualScuds.Add(manualScudEntity1);

            dbContext.LogManualScuds.Add(CreateScudLog(manualScudEntity0, autor, TypeLogEvent.Create));
            dbContext.LogManualScuds.Add(CreateScudLog(manualScudEntity1, autor, TypeLogEvent.Create));
        }
        else
        {
            ManualScudEntity manualScudEntity = new ManualScudEntity()
            {
                EmployeeId = employeeId,
                Input = begin,
                Output = end
            };

            dbContext.ManualScuds.Add(manualScudEntity);
            dbContext.LogManualScuds.Add(CreateScudLog(manualScudEntity, autor, TypeLogEvent.Create));
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> CheckPlan(Guid employeeId, DateTime begin, DateTime end)
    {
        try
        {
            using var dbContext = await _dbContextFactory.Create();

            var debugTimes = dbContext.Plans.Include(x => x.TypeOverWork).Where(x => x.EmployeeId == employeeId && begin < x.End && end > x.Begin).ToArray();
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

    private LogPlanEntity CreatePlanLog(PlanEntity plan, string autor, TypeLogEvent logEvent)
    {
        if (string.IsNullOrEmpty(autor))
        {
            autor = "n/d";
        }

        LogPlanEntity log = new LogPlanEntity()
        {
            Autor = autor,
            Begin = plan.Begin,
            End = plan.End,
            CreateAt = DateTime.Now,
            CustomTypeWorkId = plan.CustomTypeWorkId,
            EmployeeId = plan.EmployeeId,
            PlanType = plan.PlanType,
            StoreId = plan.StoreId,
            TypeLog = logEvent
        };

        return log;
    }

    private LogManualScudEntity CreateScudLog(ManualScudEntity manualScudEntity, string autor, TypeLogEvent logEvent)
    {
        LogManualScudEntity log = new LogManualScudEntity()
        {
            Autor = autor,
            CreateAt = DateTime.Now,
            EmployeeId = manualScudEntity.EmployeeId,
            Input = manualScudEntity.Input,
            Output = manualScudEntity.Output,
            TypeLog = logEvent
        };

        return log;
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

