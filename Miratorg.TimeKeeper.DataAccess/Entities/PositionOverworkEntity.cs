using System.ComponentModel.DataAnnotations.Schema;

namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class PositionOverworkEntity : BaseEntity
{
    public string Name { get; set; }
    public Guid TypeOverWorkEntityId { get; set; }

    [ForeignKey("TypeOverWorkEntityId")]
    public virtual TypeOverWorkEntity TypeOverWorkEntity { get; set; }
}
