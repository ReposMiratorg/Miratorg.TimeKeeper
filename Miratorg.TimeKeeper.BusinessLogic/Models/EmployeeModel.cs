namespace Miratorg.TimeKeeper.BusinessLogic.Models;

public class EmployeeModel
{
    public Guid EmployeeId { get; set; }
    public List<DateDetailModel> Dates { get; set; } = new List<DateDetailModel>();
}

public class DateDetailModel
{
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public PlanType PlanType { get; set; }
}

