using System.ComponentModel.DataAnnotations.Schema;

namespace Miratorg.TimeKeeper.DataAccess.Entities;

[Table("Absences", Schema = "dbo")]
public class AbsenceEntity : BaseEntity
{
    public Guid EmployeeId { get; set; }

    [ForeignKey("EmployeeId")]
    public virtual EmployeeEntity Employee { get; set; }
    public DateTime RepDate { get; set; }

    public int AbsenceCode { get; set; }
    public string AbsenceDescription { get; set; }
}