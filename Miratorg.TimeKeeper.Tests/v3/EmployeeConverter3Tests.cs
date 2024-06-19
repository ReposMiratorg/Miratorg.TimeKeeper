using Miratorg.TimeKeeper.BusinessLogic.Models;
using Miratorg.TimeKeeper.DataAccess.Entities;

namespace Miratorg.TimeKeeper.Tests.v3;

public class EmployeeConverter3Tests
{
    private Guid StoreId = Guid.NewGuid();

    [Fact]
    public void PlanDay1HourTest()
    {
        var employeeEntity = new EmployeeEntity()
        {
            StoreId = StoreId,

            Plans = new List<PlanEntity>()
            {
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 1, 3, 8, 0, 0),
                    End = new DateTime(2024, 1, 3, 20, 0, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                }
            },

            ManualScuds = new List<ManualScudEntity>()
            {
               new ManualScudEntity()
               {
                    Input = new DateTime(2024, 1, 3, 8, 0, 0),
                    Output = new DateTime(2024, 1, 3, 20, 0, 0)
               }
            },

            ScudInfos = new List<ScudInfo>()
        };

        List<SigurEventModel> sigurEvents = new List<SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(660, employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 3)]); // 12 * 60 = 720 (-1:00 обед) = 660

        var deyExportPlan = employeeModel.ExportPlanTimes.FirstOrDefault(x => x.Date == new DateOnly(2024, 1, 3));
        Assert.NotNull(deyExportPlan);
        Assert.Equal("regular", deyExportPlan.WorkTime);
        Assert.Equal(660, deyExportPlan.DayMinutes);
        Assert.Equal(0, deyExportPlan.NightMinutes);

        var deyExportFact = employeeModel.ExportFactTimes.FirstOrDefault(x => x.Date == new DateOnly(2024, 1, 3));
        Assert.NotNull(deyExportFact);
        Assert.Equal("regular", deyExportPlan.WorkTime);
        Assert.Equal(660, deyExportFact.DayMinutes);
        Assert.Equal(0, deyExportFact.NightMinutes);

        var plans = employeeModel.Plans.Where(x => x.OriginalBegin.Date == new DateTime(2024, 1, 3)).ToList();
        Assert.Equal(1, plans.Count);
        Assert.Equal(60, plans[0].ObedTimeMinutes);
        Assert.Equal(new DateTime(2024, 1, 3, 8, 0, 0), plans[0].OriginalBegin);
        Assert.Equal(new DateTime(2024, 1, 3, 8, 0, 0), plans[0].PlanCalcBegin);
        Assert.Equal(new DateTime(2024, 1, 3, 20, 0, 0), plans[0].OriginalEnd);
        Assert.Equal(new DateTime(2024, 1, 3, 19, 0, 0), plans[0].PlanCalcEnd);
    }

    [Fact]
    public void PlanDay9HourTest()
    {
        var employeeEntity = new EmployeeEntity()
        {
            StoreId = StoreId,

            Plans = new List<PlanEntity>()
            {
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 6, 6, 7, 0, 0),
                    End = new DateTime(2024, 6, 6, 19, 0, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                }
            },

            ManualScuds = new List<ManualScudEntity>()
            {
               new ManualScudEntity()
               {
                    Input = new DateTime(2024, 6, 6, 7, 0, 0),
                    Output = new DateTime(2024, 6, 6, 16, 0, 0)
               }
            },

            ScudInfos = new List<ScudInfo>()
        };

        List<SigurEventModel> sigurEvents = new List<SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(660, employeeModel.DayPlanUseMinutes[new DateTime(2024, 6, 6)]); // 12 * 60 = 720 (-1:00 обед) = 660

        var deyExportPlan = employeeModel.ExportPlanTimes.FirstOrDefault(x => x.Date == new DateOnly(2024, 6, 6));
        Assert.NotNull(deyExportPlan);
        Assert.Equal("regular", deyExportPlan.WorkTime);
        Assert.Equal(660, deyExportPlan.DayMinutes);
        Assert.Equal(0, deyExportPlan.NightMinutes);

        var deyExportFact = employeeModel.ExportFactTimes.FirstOrDefault(x => x.Date == new DateOnly(2024, 6, 6));
        Assert.NotNull(deyExportFact);
        Assert.Equal("regular", deyExportPlan.WorkTime);
        Assert.Equal(480, deyExportFact.DayMinutes);
        Assert.Equal(0, deyExportFact.NightMinutes);

        var plans = employeeModel.Plans.Where(x => x.OriginalBegin.Date == new DateTime(2024, 6, 6)).ToList();
        Assert.Equal(1, plans.Count);
        Assert.Equal(60, plans[0].ObedTimeMinutes);
        Assert.Equal(new DateTime(2024, 6, 6, 7, 0, 0), plans[0].OriginalBegin);
        Assert.Equal(new DateTime(2024, 6, 6, 7, 0, 0), plans[0].PlanCalcBegin);
        Assert.Equal(new DateTime(2024, 6, 6, 15, 0, 0), plans[0].OriginalEnd);
        Assert.Equal(new DateTime(2024, 6, 6, 16, 0, 0), plans[0].PlanCalcEnd);
    }

    [Fact]
    public void PlanDay2HourTest()
    {
        var employeeEntity = new EmployeeEntity()
        {
            StoreId = StoreId,

            Plans = new List<PlanEntity>()
            {
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 1, 3, 8, 0, 0),
                    End = new DateTime(2024, 1, 3, 20, 0, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                },
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 1, 3, 20, 0, 0),
                    End = new DateTime(2024, 1, 3, 23, 0, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Overwork,
                    TypeOverWork = new TypeOverWorkEntity()
                    {
                         Code = "administrator"
                    }
                }
            },

            ManualScuds = new List<ManualScudEntity>()
            {
               new ManualScudEntity()
               {
                    Input = new DateTime(2024, 1, 3, 8, 0, 0),
                    Output = new DateTime(2024, 1, 3, 22, 0, 0)
               }
            },

            ScudInfos = new List<ScudInfo>()
        };

        List<SigurEventModel> sigurEvents = new List<SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(840, employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 3)]); // План 12*60=720 + Подработка 3*60=180 Итого 900 (-1:00 обед 90 мин) = 840
        Assert.Equal(14 * 60, employeeModel.MountPlanUseMinuts[new DateTime(2024, 1, 1)]); // Только 1 день в тесте

        var deyExportPlans = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 1, 3)).ToList();
        Assert.NotNull(deyExportPlans);
        Assert.Equal(2, deyExportPlans.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(660, deyExportPlans[0].DayMinutes); //
        Assert.Equal(0, deyExportPlans[0].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[1].WorkTime);
        Assert.Equal(120, deyExportPlans[1].DayMinutes);
        Assert.Equal(60, deyExportPlans[1].NightMinutes);

        var deyExportFacts = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 1, 3)).ToList();
        Assert.NotNull(deyExportFacts);
        Assert.Equal(2, deyExportFacts.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(660, deyExportFacts[0].DayMinutes);
        Assert.Equal(0, deyExportFacts[0].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[1].WorkTime);
        Assert.Equal(120, deyExportFacts[1].DayMinutes);
        Assert.Equal(0, deyExportFacts[1].NightMinutes);
    }

    [Fact]
    public void PlanDay3HourTest()
    {
        var employeeEntity = new EmployeeEntity()
        {
            StoreId = StoreId,

            Plans = new List<PlanEntity>()
            {
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 6, 2, 6, 47, 0),
                    End = new DateTime(2024, 6, 2, 13, 50, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                }
            },

            ManualScuds = new List<ManualScudEntity>()
            {
               new ManualScudEntity()
               {
                    Input = new DateTime(2024, 6, 2, 7, 0, 0),
                    Output = new DateTime(2024, 6, 2, 13, 50, 0)
               }
            },

            ScudInfos = new List<ScudInfo>()
        };

        List<SigurEventModel> sigurEvents = new List<SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(393, employeeModel.DayPlanUseMinutes[new DateTime(2024, 6, 2)]); // План 423 - 30 = 393
        //Assert.(6.33, employeeModel.MountPlanUseHours[new DateTime(2024, 6, 2)]); // Только 1 день в тесте

        var deyExportPlans = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 6, 2)).ToList();
        Assert.NotNull(deyExportPlans);
        Assert.Equal(1, deyExportPlans.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(393, deyExportPlans[0].DayMinutes); //
        Assert.Equal(0, deyExportPlans[0].NightMinutes);

        var deyExportFacts = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 6, 2)).ToList();
        Assert.NotNull(deyExportFacts);
        Assert.Equal(1, deyExportFacts.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(380, deyExportFacts[0].DayMinutes);
        Assert.Equal(0, deyExportFacts[0].NightMinutes);
    }

    [Fact]
    public void PlanDay4HourTest()
    {
        // вход и выход опреелдляются через сигур
        var employeeEntity = new EmployeeEntity()
        {
            StoreId = StoreId,

            Plans = new List<PlanEntity>()
            {
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 6, 2, 6, 47, 0),
                    End = new DateTime(2024, 6, 2, 13, 50, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                }
            },

            ManualScuds = new List<ManualScudEntity>(),
            ScudInfos = new List<ScudInfo>()
        };

        List<SigurEventModel> sigurEvents = new List<SigurEventModel>()
        {
            new SigurEventModel() { CodeNav = string.Empty, EventTime = new DateTime(2024, 6, 2, 7, 0, 0) },
            new SigurEventModel() { CodeNav = string.Empty, EventTime = new DateTime(2024, 6, 2, 13, 50, 0) }
        };

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(393, employeeModel.DayPlanUseMinutes[new DateTime(2024, 6, 2)]); // План 423 - 30 = 393
        //Assert.(6.33, employeeModel.MountPlanUseHours[new DateTime(2024, 6, 2)]); // Только 1 день в тесте

        var deyExportPlans = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 6, 2)).ToList();
        Assert.NotNull(deyExportPlans);
        Assert.Equal(1, deyExportPlans.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(393, deyExportPlans[0].DayMinutes); //
        Assert.Equal(0, deyExportPlans[0].NightMinutes);

        var deyExportFacts = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 6, 2)).ToList();
        Assert.NotNull(deyExportFacts);
        Assert.Equal(1, deyExportFacts.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(380, deyExportFacts[0].DayMinutes);
        Assert.Equal(0, deyExportFacts[0].NightMinutes);
    }

    [Fact]
    public void PlanDay5HourTest()
    {
        // вход и выход опреелдляются через сигур (переходящее рабочее время на другое число)
        var employeeEntity = new EmployeeEntity()
        {
            StoreId = StoreId,

            Plans = new List<PlanEntity>()
            {
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 6, 2, 8, 00, 0),
                    End = new DateTime(2024, 6, 2, 23, 59, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                },

                new PlanEntity()
                {
                    Begin = new DateTime(2024, 6, 3, 0, 0, 0),
                    End = new DateTime(2024, 6, 3, 7, 00, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                }
            },

            ManualScuds = new List<ManualScudEntity>(),
            ScudInfos = new List<ScudInfo>()
        };

        List<SigurEventModel> sigurEvents = new List<SigurEventModel>()
        {
            new SigurEventModel() { CodeNav = string.Empty, EventTime = new DateTime(2024, 6, 2, 8, 0, 0) },
            new SigurEventModel() { CodeNav = string.Empty, EventTime = new DateTime(2024, 6, 3, 7, 0, 0) }
        };

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(899, employeeModel.DayPlanUseMinutes[new DateTime(2024, 6, 2)]); // должно быть (16 * 60) - 90 = 960 - 60 = 900 ~ 899
        Assert.Equal(420, employeeModel.DayPlanUseMinutes[new DateTime(2024, 6, 3)]); // не верное время - проверить -> должно быть 420

        var deyExportPlans2 = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 6, 2)).ToList();
        Assert.NotNull(deyExportPlans2);
        Assert.Equal(1, deyExportPlans2.Count);
        Assert.Equal("regular", deyExportPlans2[0].WorkTime);
        Assert.Equal(840, deyExportPlans2[0].DayMinutes); // не верное время - проверить
        Assert.Equal(119, deyExportPlans2[0].NightMinutes); // не верное время - проверить -> должно быть 119 минут ночью

        var deyExportFacts2 = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 6, 2)).ToList();
        Assert.NotNull(deyExportFacts2);
        Assert.Equal(1, deyExportFacts2.Count);
        Assert.Equal("regular", deyExportFacts2[0].WorkTime);
        Assert.Equal(840, deyExportFacts2[0].DayMinutes);
        Assert.Equal(119, deyExportFacts2[0].NightMinutes);


        var deyExportPlans3 = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 6, 3)).ToList();
        Assert.NotNull(deyExportPlans3);
        Assert.Equal(1, deyExportPlans3.Count);
        Assert.Equal("regular", deyExportPlans3[0].WorkTime);
        Assert.Equal(60, deyExportPlans3[0].DayMinutes); //
        Assert.Equal(360, deyExportPlans3[0].NightMinutes);

        var deyExportFacts3 = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 6, 3)).ToList();
        Assert.NotNull(deyExportFacts3);
        Assert.Equal(1, deyExportFacts3.Count);
        Assert.Equal("regular", deyExportFacts3[0].WorkTime);
        Assert.Equal(60, deyExportFacts3[0].DayMinutes);
        Assert.Equal(360, deyExportFacts3[0].NightMinutes);
    }
}
