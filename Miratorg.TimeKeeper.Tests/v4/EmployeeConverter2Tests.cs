using Miratorg.TimeKeeper.DataAccess.Entities;

namespace Miratorg.TimeKeeper.Tests.v4;

public class EmployeeConverter2Tests
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

        List<BusinessLogic.Models.SigurEventModel> sigurEvents = new List<BusinessLogic.Models.SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV4(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(660, employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 3)]); // 12 * 60 = 720 (-1:00 обед) = 660
        Assert.Equal(660, employeeModel.DayScudUseMinutes[new DateTime(2024, 1, 3)]); // 12 * 60 = 720 (-1:00 обед) = 660
        Assert.Equal(660, employeeModel.MountScudUseMinutes[new DateTime(2024, 1, 1)]); // 12 * 60 = 720 (-1:00 обед) = 660

        var deyExportPlan = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 1, 3)).ToList();
        Assert.Equal(1, deyExportPlan.Count);
        Assert.NotNull(deyExportPlan);
        Assert.Equal("regular", deyExportPlan[0].WorkTime);
        Assert.Equal(660, deyExportPlan[0].DayMinutes);
        Assert.Equal(0, deyExportPlan[0].NightMinutes);

        var deyExportFact = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 1, 3)).ToList();
        Assert.Equal(1, deyExportFact.Count);
        Assert.NotNull(deyExportFact);
        Assert.Equal("regular", deyExportPlan[0].WorkTime);
        Assert.Equal(660, deyExportFact[0].DayMinutes);
        Assert.Equal(0, deyExportFact[0].NightMinutes);
    }

    [Fact]
    public void PlanDay12HourTest()
    {
        // Должно работать, но не работает расчет ночных часов
        var employeeEntity = new EmployeeEntity()
        {
            StoreId = StoreId,

            Plans = new List<PlanEntity>()
            {
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 1, 3, 8, 0, 0),
                    End = new DateTime(2024, 1, 3, 22, 0, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                },
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 1, 3, 22, 0, 0),
                    End = new DateTime(2024, 1, 3, 23, 0, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                }
            },

            ManualScuds = new List<ManualScudEntity>()
            {
               new ManualScudEntity()
               {
                    Input =  new DateTime(2024, 1, 3,  7, 33, 0),
                    Output = new DateTime(2024, 1, 3, 23,  0, 0)
               }
            },

            ScudInfos = new List<ScudInfo>()
        };

        List<BusinessLogic.Models.SigurEventModel> sigurEvents = new List<BusinessLogic.Models.SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV4(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(840, employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 3)]); // 12 * 60 = 720 (-1:00 обед) = 660
        Assert.Equal(840, employeeModel.DayScudUseMinutes[new DateTime(2024, 1, 3)]); // 12 * 60 = 720 (-1:00 обед) = 660
        Assert.Equal(840, employeeModel.MountScudUseMinutes[new DateTime(2024, 1, 1)]); // 12 * 60 = 720 (-1:00 обед) = 660

        var deyExportPlan = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 1, 3)).ToList();
        Assert.NotNull(deyExportPlan);
        Assert.Equal(2, deyExportPlan.Count);
        Assert.Equal("regular", deyExportPlan[0].WorkTime);
        Assert.Equal(780, deyExportPlan[0].DayMinutes);
        Assert.Equal(0, deyExportPlan[0].NightMinutes);
        Assert.Equal("regular", deyExportPlan[1].WorkTime);
        Assert.Equal(0, deyExportPlan[1].DayMinutes);
        Assert.Equal(60, deyExportPlan[1].NightMinutes);

        var deyExportFact = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 1, 3)).ToList();
        Assert.NotNull(deyExportFact);
        Assert.Equal(2, deyExportFact.Count);
        Assert.Equal("regular", deyExportFact[0].WorkTime);
        Assert.Equal(780, deyExportFact[0].DayMinutes);
        Assert.Equal(0, deyExportFact[0].NightMinutes);
        Assert.Equal("regular", deyExportFact[1].WorkTime);
        Assert.Equal(0, deyExportFact[1].DayMinutes);
        Assert.Equal(60, deyExportFact[1].NightMinutes);
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
                },
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 1, 4, 8, 0, 0),
                    End = new DateTime(2024, 1, 4, 20, 0, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                },
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 1, 4, 20, 0, 0),
                    End = new DateTime(2024, 1, 4, 23, 0, 0),
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
               },
               new ManualScudEntity()
               {
                    Input = new DateTime(2024, 1, 4, 8, 0, 0),
                    Output = new DateTime(2024, 1, 4, 22, 0, 0)
               }
            },

            ScudInfos = new List<ScudInfo>()
        };

        List<BusinessLogic.Models.SigurEventModel> sigurEvents = new List<BusinessLogic.Models.SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV4(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(840, employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 3)]); // План 12*60=720 + Подработка 3*60=180 Итого 900 (-1:00 обед 90 мин) = 840
        Assert.Equal(14 * 60*2, employeeModel.MountPlanUseMinuts[new DateTime(2024, 1, 1)]); // Только 1 день в тесте
        Assert.Equal(13 * 60, employeeModel.DayScudUseMinutes[new DateTime(2024, 1, 3)]); // Только 1 день в тесте
        Assert.Equal(13 * 60*2, employeeModel.MountScudUseMinutes[new DateTime(2024, 1, 1)]); // Только 2 день в тесте

        Assert.Equal(840, employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 4)]); // План 12*60=720 + Подработка 3*60=180 Итого 900 (-1:00 обед 90 мин) = 840
        Assert.Equal(14 * 60*2, employeeModel.MountPlanUseMinuts[new DateTime(2024, 1, 1)]); // Только 1 день в тесте
        Assert.Equal(13 * 60, employeeModel.DayScudUseMinutes[new DateTime(2024, 1, 4)]); // Только 1 день в тесте
        Assert.Equal(13 * 60*2, employeeModel.MountScudUseMinutes[new DateTime(2024, 1, 1)]); // Только 2 день в тесте

        var deyExportPlans = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 1, 3)).ToList();
        Assert.NotNull(deyExportPlans);
        Assert.Equal(3, deyExportPlans.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(660, deyExportPlans[0].DayMinutes); //
        Assert.Equal(0, deyExportPlans[0].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[1].WorkTime);
        Assert.Equal(120, deyExportPlans[1].DayMinutes); //
        Assert.Equal(0, deyExportPlans[1].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[2].WorkTime);
        Assert.Equal(0, deyExportPlans[2].DayMinutes);
        Assert.Equal(60, deyExportPlans[2].NightMinutes);

        deyExportPlans = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 1, 4)).ToList();
        Assert.NotNull(deyExportPlans);
        Assert.Equal(3, deyExportPlans.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(660, deyExportPlans[0].DayMinutes); //
        Assert.Equal(0, deyExportPlans[0].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[1].WorkTime);
        Assert.Equal(120, deyExportPlans[1].DayMinutes); //
        Assert.Equal(0, deyExportPlans[1].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[2].WorkTime);
        Assert.Equal(0, deyExportPlans[2].DayMinutes);
        Assert.Equal(60, deyExportPlans[2].NightMinutes);

        var deyExportFacts = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 1, 3)).ToList();
        Assert.NotNull(deyExportFacts);
        Assert.Equal(3, deyExportFacts.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(660, deyExportFacts[0].DayMinutes);
        Assert.Equal(0, deyExportFacts[0].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[1].WorkTime);
        Assert.Equal(120, deyExportFacts[1].DayMinutes);
        Assert.Equal(0, deyExportFacts[1].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[2].WorkTime);
        Assert.Equal(0, deyExportFacts[2].DayMinutes);
        Assert.Equal(0, deyExportFacts[2].NightMinutes);

        deyExportFacts = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 1, 4)).ToList();
        Assert.NotNull(deyExportFacts);
        Assert.Equal(3, deyExportFacts.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(660, deyExportFacts[0].DayMinutes);
        Assert.Equal(0, deyExportFacts[0].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[1].WorkTime);
        Assert.Equal(120, deyExportFacts[1].DayMinutes);
        Assert.Equal(0, deyExportFacts[1].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[2].WorkTime);
        Assert.Equal(0, deyExportFacts[2].DayMinutes);
        Assert.Equal(0, deyExportFacts[2].NightMinutes);
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
                },
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 6, 3, 6, 47, 0),
                    End = new DateTime(2024, 6, 3, 13, 50, 0),
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
               },
               new ManualScudEntity()
               {
                    Input = new DateTime(2024, 6, 3, 7, 0, 0),
                    Output = new DateTime(2024, 6, 3, 13, 50, 0)
               }
            },

            ScudInfos = new List<ScudInfo>()
        };

        List<BusinessLogic.Models.SigurEventModel> sigurEvents = new List<BusinessLogic.Models.SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV4(employeeEntity, sigurEvents);

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

        deyExportPlans = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 6, 3)).ToList();
        Assert.NotNull(deyExportPlans);
        Assert.Equal(1, deyExportPlans.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(393, deyExportPlans[0].DayMinutes); //
        Assert.Equal(0, deyExportPlans[0].NightMinutes);

        deyExportFacts = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 6, 3)).ToList();
        Assert.NotNull(deyExportFacts);
        Assert.Equal(1, deyExportFacts.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(380, deyExportFacts[0].DayMinutes);
        Assert.Equal(0, deyExportFacts[0].NightMinutes);
    }
}
