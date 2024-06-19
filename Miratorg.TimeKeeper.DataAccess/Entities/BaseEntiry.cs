namespace Miratorg.TimeKeeper.DataAccess.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
}


public abstract class LogBaseEntity : BaseEntity
{
    public DateTime CreateAt { get; set; }
    public TypeLogEvent TypeLog {  get; set; }
    public string Autor { get; set; }
}

public enum TypeLogEvent
{
    Create = 0,
    Update = 1,
    Delete = 2
}