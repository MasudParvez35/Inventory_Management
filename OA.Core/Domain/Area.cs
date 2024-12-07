using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Core.Domain;

public class Area : BaseEntity
{
    public string Name { get; set; }

    public int CityId { get; set; }

    public int DisplayOrder { get; set; }

    #region Nagivate Properties

    [ForeignKey("CityId")]
    public City? City { get; set; }

    #endregion
}
