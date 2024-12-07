using OA.Core;

namespace OA_WEB.Areas.Admin.Models;

public class CityModel : BaseEntity
{
    public string Name { get; set; }

    public int DisplayOrder { get; set; }

    public int NumberOfAreas { get; set; }
}
