namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class PlanEntity : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public virtual EmployeeEntity Employee { get; set; }
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public PlanType PlanType { get; set; }
    public Guid? StoreId { get; set; }
    //public virtual StoreEntity? Store { get; set; }
}

public enum PlanType
{
    Plan = 1,
    Overwork = 2
}
