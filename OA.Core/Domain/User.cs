using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Core.Domain;

public class User : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Phone { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    public int AreaId { get; set; }

    #region Navigation Properties

    [ForeignKey("StateId")]
    public State? State { get; set; }

    [ForeignKey("CityId")]
    public City? City { get; set; }

    [ForeignKey("AreaId")]
    public Area? Area { get; set; }

    #endregion
}
