using Miratorg.TimeKeeper.DataAccess.Entities;

namespace Miratorg.TimeKeeper.Tests;

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

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV2(employeeEntity);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(630, employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 3)]); // 12 * 60 = 720 (-1:30 обед) = 630

        var deyExportPlan = employeeModel.ExportPlanTimes.FirstOrDefault(x => x.Date == new DateOnly(2024, 1, 3));
        Assert.NotNull(deyExportPlan);
        Assert.Equal("regular", deyExportPlan.WorkTime);
        Assert.Equal(630, deyExportPlan.DayMinutes);
        Assert.Equal(0, deyExportPlan.NightMinutes);

        var deyExportFact = employeeModel.ExportFactTimes.FirstOrDefault(x => x.Date == new DateOnly(2024, 1, 3));
        Assert.NotNull(deyExportFact);
        Assert.Equal("regular", deyExportPlan.WorkTime);
        Assert.Equal(630, deyExportFact.DayMinutes);
        Assert.Equal(0, deyExportFact.NightMinutes);
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

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV2(employeeEntity);

        Assert.NotNull(employeeModel);
        Assert.Equal(StoreId, employeeModel.StoreId);
        Assert.Equal(810, employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 3)]); // План 12*60=720 + Подработка 3*60=180 Итого 900 (-1:30 обед 90 мин) = 810
        Assert.Equal(13.5, employeeModel.MountPlanUseHours[new DateTime(2024, 1, 1)]); // Только 1 день в тесте

        var deyExportPlans = employeeModel.ExportPlanTimes.Where(x => x.Date == new DateOnly(2024, 1, 3)).ToList();
        Assert.NotNull(deyExportPlans);
        Assert.Equal(2, deyExportPlans.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(630, deyExportPlans[0].DayMinutes); //
        Assert.Equal(0, deyExportPlans[0].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[1].WorkTime);
        Assert.Equal(120, deyExportPlans[1].DayMinutes);
        Assert.Equal(60, deyExportPlans[1].NightMinutes);

        var deyExportFacts = employeeModel.ExportFactTimes.Where(x => x.Date == new DateOnly(2024, 1, 3)).ToList();
        Assert.NotNull(deyExportFacts);
        Assert.Equal(2, deyExportFacts.Count);
        Assert.Equal("regular", deyExportPlans[0].WorkTime);
        Assert.Equal(630, deyExportFacts[0].DayMinutes);
        Assert.Equal(0, deyExportFacts[0].NightMinutes);
        Assert.Equal("administrator", deyExportPlans[1].WorkTime);
        Assert.Equal(120, deyExportFacts[1].DayMinutes);
        Assert.Equal(0, deyExportFacts[1].NightMinutes);
    }
}
