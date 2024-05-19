namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class EmployeeEntity : BaseEntity
{
    public string Name { get; set; }
    public string Position { get; set; }
    public string CodeNav { get; set; }

    public Guid Guid1C { get; set; }

    public Guid? StoreId { get; set; }
    public virtual StoreEntity? Store { get; set; }

    public Guid? BossId { get; set; }
    public virtual EmployeeEntity? Boss { get; set; }

    public Guid? ScheduleId { get; set; }
    public virtual ScheduleEntity? Schedule { get; set; }

    public virtual List<ScudInfo> ScudInfos { get; set; }
    public virtual List<PlanEntity> Plans { get; set; }
    public virtual List<AbsenceEntity> Absences { get; set; }
}
