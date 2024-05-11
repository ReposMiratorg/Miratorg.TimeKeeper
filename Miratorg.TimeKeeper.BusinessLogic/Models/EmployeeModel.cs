namespace Miratorg.TimeKeeper.BusinessLogic.Models;

public class EmployeeModel
{
    public Guid EmployeeId { get; set; }
    public string Name { get; set; }
    public Guid? StoreId { get; set; }
    public List<PlanDetailModel> Plans { get; set; } = new List<PlanDetailModel>();
    public List<Schedule1CPlanModel> WorkDates { get; set; } = new List<Schedule1CPlanModel>();
    public List<ScudInfoModel> ScudInfos { get; set; } = new List<ScudInfoModel>();
}

public class PlanDetailModel
{
    public Guid Id { get; set; }
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public PlanType PlanType { get; set; }
    public Guid? StoreId { get; set; }
}

public class Schedule1CPlanModel
{
    public DateTime Begin { get; set; }
    public DateTime? End { get; set; }
}

public class ScudInfoModel
{
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
}

