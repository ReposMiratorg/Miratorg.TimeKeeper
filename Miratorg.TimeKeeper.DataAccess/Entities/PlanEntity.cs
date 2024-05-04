namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class PlanEntity : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public virtual EmployeeEntity Employee { get; set; }
    public DateTime DateKey { get; set; }
    public DateTime? DateInput { get; set; }
    public DateTime? DateOutput { get; set; }
    public PlanType PlanType { get; set; }
}

public enum PlanType
{
    Plan = 1,
    Overwork = 2,
}
