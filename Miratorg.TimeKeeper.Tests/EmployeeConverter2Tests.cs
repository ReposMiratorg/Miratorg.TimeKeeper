using Miratorg.TimeKeeper.DataAccess.Entities;

namespace Miratorg.TimeKeeper.Tests;

public class EmployeeConverter2Tests
{
    private Guid StoreId = Guid.NewGuid();

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
}
