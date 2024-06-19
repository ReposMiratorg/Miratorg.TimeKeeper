using System.ComponentModel.DataAnnotations.Schema;

namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class PlanEntity : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public virtual EmployeeEntity Employee { get; set; }
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public PlanType PlanType { get; set; }
    public Guid StoreId { get; set; }
    //public virtual StoreEntity? Store { get; set; }
    public Guid? CustomTypeWorkId { get; set; }

    [ForeignKey("CustomTypeWorkId")]
    public virtual CustomTypeWorkEntity? CustomTypeWork { get; set; }

    public Guid? TypeOverWorkId { get; set; }

    [ForeignKey("TypeOverWorkId")]
    public virtual TypeOverWorkEntity TypeOverWork { get; set; }
}

public enum PlanType
{
    Plan = 1,
    Overwork = 2
}

public class LogPlanEntity : LogBaseEntity
{
    public Guid EmployeeId { get; set; }
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public PlanType PlanType { get; set; }
    public Guid StoreId { get; set; }
    public Guid? CustomTypeWorkId { get; set; }
    public Guid? TypeOverWorkId { get; set; }
}
