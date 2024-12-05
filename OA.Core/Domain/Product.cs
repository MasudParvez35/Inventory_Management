using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Core.Domain;

public class Product : BaseEntity
{
    public string Name { get; set; }

    public string? ImagePath { get; set; }

    public string Description { get; set; }

    public decimal BuyingPrice { get; set; }

    public decimal SellingPrice { get; set; }

    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    public int WarehouseId { get; set; }

    #region Nagivate Properties

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
    
    [ForeignKey("WarehouseId")]
    public Warehouse? Warehouse { get; set; }

    #endregion
}
