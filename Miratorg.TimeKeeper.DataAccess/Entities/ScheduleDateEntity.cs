namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class ScheduleDateEntity : BaseEntity
{
    public Guid ScheduleId { get; set; }
    public virtual ScheduleEntity Schedule { get; set; }
    public DateTime TimeBegin { get; set; }
    public DateTime? TimeEnd { get; set; }
}
