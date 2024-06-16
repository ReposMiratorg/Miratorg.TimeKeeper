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

        List<BusinessLogic.Models.SigurEventModel> sigurEvents = new List<BusinessLogic.Models.SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(120, employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 3)]);
        Assert.Equal(0, employeeModel.DayScudUseMinutes[new DateTime(2024, 1, 3)]);
        Assert.Equal(0, employeeModel.MountScudUseMinutes[new DateTime(2024, 1, 1)]);
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

        List<BusinessLogic.Models.SigurEventModel> sigurEvents = new List<BusinessLogic.Models.SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(180, employeeModel.DayPlanUseMinutes[new DateTime(2024, 1, 1)]);
        Assert.Equal(0, employeeModel.DayScudUseMinutes[new DateTime(2024, 1, 1)]);
        Assert.Equal(0, employeeModel.MountScudUseMinutes[new DateTime(2024, 1, 1)]);
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

        List<BusinessLogic.Models.SigurEventModel> sigurEvents = new List<BusinessLogic.Models.SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(240, employeeModel.DayPlanUseMinutes[new DateTime(2024, 3, 8)]);
        Assert.Equal(0, employeeModel.DayScudUseMinutes[new DateTime(2024, 3, 8)]);
        Assert.Equal(0, employeeModel.MountScudUseMinutes[new DateTime(2024, 1, 1)]);
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

        List<BusinessLogic.Models.SigurEventModel> sigurEvents = new List<BusinessLogic.Models.SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(0, employeeModel.DayPlanUseMinutes[new DateTime(2024, 3, 8)]);
        Assert.Equal(0, employeeModel.DayScudUseMinutes[new DateTime(2024, 3, 8)]);
        Assert.Equal(0, employeeModel.MountScudUseMinutes[new DateTime(2024, 3, 1)]);
    }

    [Fact]
    public void ManualScudDay1HourTest()
    {
        var employeeEntity = new EmployeeEntity()
        {
            Plans = new List<PlanEntity>(),
            ManualScuds = new List<ManualScudEntity>()
            {
                new ManualScudEntity()
                {
                    Input = new DateTime(2024, 3, 8, 8, 0, 0),
                    Output = new DateTime(2024, 3, 8, 9, 0, 0)
                }
            },
            ScudInfos = new List<ScudInfo>()
        };

        List<BusinessLogic.Models.SigurEventModel> sigurEvents = new List<BusinessLogic.Models.SigurEventModel>();

        var employeeModel = BusinessLogic.TimeKeeperConverter.ConvertV3(employeeEntity, sigurEvents);

        Assert.NotNull(employeeModel);
        Assert.Equal(0, employeeModel.DayPlanUseMinutes[new DateTime(2024, 3, 8)]);
        Assert.Equal(0, employeeModel.DayScudUseMinutes[new DateTime(2024, 3, 8)]);
        Assert.Equal(0, employeeModel.MountScudUseMinutes[new DateTime(2024, 3, 1)]);
    }
}
