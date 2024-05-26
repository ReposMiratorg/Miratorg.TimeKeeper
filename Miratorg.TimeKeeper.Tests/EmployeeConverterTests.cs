using Miratorg.TimeKeeper.DataAccess.Entities;

namespace Miratorg.TimeKeeper.Tests;

public class EmployeeConverterTests
{
    private Guid StoreId = Guid.NewGuid();

    [Fact]
    public void PlanDay2HourTest()
    {
        var employeeEntity = new EmployeeEntity()
        {
            Plans = new List<PlanEntity>()
            {
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 1, 3, 8, 0, 0),
                    End = new DateTime(2024, 1, 3, 10, 0, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                }
            },

            ManualScuds = new List<ManualScudEntity>(),
            ScudInfos = new List<ScudInfo>()
        };

        var employeeModel = BusinessLogic.TimeKeeperConverter.Convert(employeeEntity);

        Assert.NotNull(employeeModel);
        Assert.Equal(employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 3)], 120);
    }

    [Fact]
    public void PlanDay3HourTest()
    {
        var employeeEntity = new EmployeeEntity()
        {
            Plans = new List<PlanEntity>()
            {
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 1, 1, 8, 0, 0),
                    End = new DateTime(2024, 1, 1, 11, 0, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                }
            },

            ManualScuds = new List<ManualScudEntity>(),
            ScudInfos = new List<ScudInfo>()
        };

        var employeeModel = BusinessLogic.TimeKeeperConverter.Convert(employeeEntity);

        Assert.NotNull(employeeModel);
        Assert.Equal(employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 1)], 120);
    }

    [Fact]
    public void PlanDay4HourTest()
    {
        var employeeEntity = new EmployeeEntity()
        {
            Plans = new List<PlanEntity>()
            {
                new PlanEntity()
                {
                    Begin = new DateTime(2024, 3, 8, 8, 0, 0),
                    End = new DateTime(2024, 3, 8, 12, 0, 0),
                    StoreId = StoreId,
                    PlanType = PlanType.Plan
                }
            },

            ManualScuds = new List<ManualScudEntity>(),
            ScudInfos = new List<ScudInfo>()
        };

        var employeeModel = BusinessLogic.TimeKeeperConverter.Convert(employeeEntity);

        Assert.NotNull(employeeModel);
        Assert.Equal(employeeModel.DayPlanUseMinutes[new DateTime(2024, 3, 8)], 180);
    }


    [Fact]
    public void ScudDay1HourTest()
    {
        var employeeEntity = new EmployeeEntity()
        {
            Plans = new List<PlanEntity>(),
            ManualScuds = new List<ManualScudEntity>(),
            ScudInfos = new List<ScudInfo>()
            {
                new ScudInfo()
                {
                    Input = new DateTime(2024, 3, 8, 8, 0, 0),
                    Output = new DateTime(2024, 3, 8, 9, 0, 0)
                }
            }
        };

        var employeeModel = BusinessLogic.TimeKeeperConverter.Convert(employeeEntity);

        Assert.NotNull(employeeModel);
        Assert.Equal(employeeModel.DayPlanUseMinutes[new DateTime(2024, 3, 8)], 0);
        Assert.Equal(employeeModel.DayScudUseMinutes[new DateTime(2024, 3, 8)], 60);
    }
}
