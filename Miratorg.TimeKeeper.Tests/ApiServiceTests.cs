using Miratorg.TimeKeeper.BusinessLogic.Models;
using Miratorg.TimeKeeper.BusinessLogic.Services;

namespace Miratorg.TimeKeeper.Tests;

public class ApiServiceTests
{
    [Fact]
    public void Day1Test()
    {
        var begin = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 1, 1);
        
        var storeId =  Guid.NewGuid();
        var model = new EmployeeModel()
        {
            Plans = new List<PlanDetailModel>()
            {
                 new PlanDetailModel()
                 {
                      Begin = new DateTime(2024, 1, 1, 7, 0,0),
                      End = new DateTime(2024, 1, 1, 8, 0, 0),
                      PlanType = DataAccess.Entities.PlanType.Plan,
                      StoreId = storeId,
                      TypeOverWorkName = "rr"
                 }
            }
        };

        var results = ApiService.ConverToTimesheet(model, begin, end, storeId, "storeCode1C");

        Assert.Equal(1, results.Count);
        Assert.Equal("2024-01-01", results[0].date);
        Assert.Equal("storeCode1C", results[0].employeeId);
        Assert.NotEqual("N/D", results[0].worktime[0].type);
        Assert.NotEqual("rr", results[0].worktime[0].type);
        Assert.Equal(60, results[0].worktime[0].dvalue);
        //Assert.Equal(1, results[0].date == begin.ToString("yyyy-MM-dd"));
    }


    [Fact]
    public void Day6Test()
    {
        var begin = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 1, 1);

        var storeId = Guid.NewGuid();
        var model = new EmployeeModel()
        {
            Plans = new List<PlanDetailModel>()
            {
                 new PlanDetailModel()
                 {
                      Begin = new DateTime(2024, 1, 1, 7, 0,0),
                      End = new DateTime(2024, 1, 1, 13, 0, 0),
                      PlanType = DataAccess.Entities.PlanType.Plan,
                      StoreId = storeId,
                      TypeOverWorkName = "rr"
                 }
            }
        };

        var results = ApiService.ConverToTimesheet(model, begin, end, storeId, "storeCode1C");

        Assert.Equal(1, results.Count);
        Assert.Equal("2024-01-01", results[0].date);
        Assert.Equal("storeCode1C", results[0].employeeId);
        Assert.NotEqual("N/D", results[0].worktime[0].type);
        Assert.Equal(300, results[0].worktime[0].dvalue);
        //Assert.Equal(1, results[0].date == begin.ToString("yyyy-MM-dd"));
    }

    [Fact]
    public void Over8Test()
    {
        //Тест удалеия часа из 8и часовой ерербот
        var begin = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 1, 1);

        var storeId = Guid.NewGuid();
        var model = new EmployeeModel()
        {
            Plans = new List<PlanDetailModel>()
            {
                 new PlanDetailModel()
                 {
                      Begin = new DateTime(2024, 1, 1, 7, 0,0),
                      End = new DateTime(2024, 1, 1, 18, 0, 0),
                      PlanType = DataAccess.Entities.PlanType.Overwork,
                      StoreId = storeId,
                      TypeOverWorkName = "rr"
                 }
            }
        };

        var results = ApiService.ConverToTimesheet(model, begin, end, storeId, "storeCode1C");

        Assert.Equal(1, results.Count);
        Assert.Equal("2024-01-01", results[0].date);
        Assert.Equal("storeCode1C", results[0].employeeId);
        Assert.NotEqual("N/D", results[0].worktime[0].type);
        Assert.Equal(540, results[0].worktime[0].dvalue);
        Assert.Equal(540, results[0].dovertime);
        //Assert.Equal(1, results[0].date == begin.ToString("yyyy-MM-dd"));
    }
}
