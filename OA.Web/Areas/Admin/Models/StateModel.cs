using OA.Core;
namespace OA_WEB.Areas.Admin.Models;

public class StateModel : BaseEntity
{
    public StateModel()
    {
        Cities = new List<CityModel>();
    }

    public string Name { get; set; }

    public int NumberOfCity { get; set; }

    public int DisplayOrder { get; set; }

    public IList<CityModel> Cities { get; set; }
}
