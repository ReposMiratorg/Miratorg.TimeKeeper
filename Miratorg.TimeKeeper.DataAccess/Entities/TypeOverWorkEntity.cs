using System.ComponentModel.DataAnnotations.Schema;

namespace Miratorg.TimeKeeper.DataAccess.Entities;

public class TypeOverWorkEntity : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }

    public virtual List<PositionOverworkEntity> PositionOverworks { get; set; }
}
