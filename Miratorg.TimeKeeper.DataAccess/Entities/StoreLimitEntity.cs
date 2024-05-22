namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class StoreLimitEntity : BaseEntity
{
    public Guid StoreId { get; set; }
    public StoreEntity Store { get; set; }
    public int Year { get; set; }
    public int Mouth { get; set; }
    public int Limit { get; set; }
}
