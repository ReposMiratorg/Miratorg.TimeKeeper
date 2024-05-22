namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class StoreEntity : BaseEntity
{
    public string Name { get; set; }
    public Guid StoreId1C { get; set; }
    public virtual List<StoreLimitEntity> Limits { get; set; }
}
