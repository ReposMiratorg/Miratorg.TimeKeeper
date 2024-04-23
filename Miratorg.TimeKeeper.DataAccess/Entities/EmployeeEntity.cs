namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class EmployeeEntity : BaseEntity
{
    public string Name { get; set; }
    public virtual StoreEntity? Store { get; set; }
    public string CodeNav { get; set; }
    public string Division { get; set; }

    public Guid? BossId { get; set; }
    public virtual EmployeeEntity? Boss { get; set; }

    public Guid? ScheduleId { get; set; }
    public virtual ScheduleEntity? ScheduleEntity { get; set; }
}

public class StoreEntity : BaseEntity
{
    public string Name { get; set; }
}
