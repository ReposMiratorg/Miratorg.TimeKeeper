namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class SigurInfoEntity : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }

    public string CodeNav {  get; set; }
    public DateTime EventTime { get; set; }
}
