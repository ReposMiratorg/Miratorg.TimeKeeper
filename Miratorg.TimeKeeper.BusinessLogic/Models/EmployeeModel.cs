namespace Miratorg.TimeKeeper.BusinessLogic.Models;

public class EmployeeModel
{
    public Guid EmployeeId { get; set; }
    public List<DateDetailModel> Dates { get; set; } = new List<DateDetailModel>();
    public List<Schedule1CPlanModel> WorkDates { get; set; } = new List<Schedule1CPlanModel>();
}

public class DateDetailModel
{
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public PlanType PlanType { get; set; }
}

public class Schedule1CPlanModel
{
    public DateTime Begin { get; set; }
    public DateTime? End { get; set; }
}

