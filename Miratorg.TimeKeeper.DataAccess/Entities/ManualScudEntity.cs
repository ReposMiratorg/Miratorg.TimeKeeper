namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class ManualScudEntity : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
    public DateTime Input { get; set; }
    public DateTime Output { get; set; }
}


public class LogManualScudEntity : LogBaseEntity
{
    public Guid EmployeeId { get; set; }
    public DateTime Input { get; set; }
    public DateTime Output { get; set; }
}