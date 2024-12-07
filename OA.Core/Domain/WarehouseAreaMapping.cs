using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Core.Domain;

public class WarehouseAreaMapping : BaseEntity
{
    public int WarehouseId { get; set; }

    public int AreaId { get; set; }

    #region Navigation Properties

    [ForeignKey("WarehouseId")]
    public Warehouse? Warehouse { get; set; }
    
    [ForeignKey("AreaId")]
    public Area? Area { get; set; }

    #endregion
}
