using OA.Core;

namespace OA_WEB.Areas.Admin.Models;

public class CityListModel : BaseEntity
{
    public CityListModel()
    {
        Cities = new List<CityModel>();
    }

    public IList<CityModel> Cities { get; set; }
}
