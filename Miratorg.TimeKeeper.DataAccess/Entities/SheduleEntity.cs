namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class ScheduleEntity : BaseEntity
{
    public string Name { get; set; }
    public int Code { get; set; }
    public List<ScheduleDateEntity> Dates { get; set; } = [];
    public List<EmployeeEntity> Employees { get; set; } = [];
}
