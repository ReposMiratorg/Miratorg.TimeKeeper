namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class ScudInfo : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
    public DateTime Input { get; set; }
    public DateTime Output { get; set; }
}
