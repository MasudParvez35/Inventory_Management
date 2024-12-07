using OA.Core;

namespace OA_WEB.Areas.Admin.Models;

public class AreaListModel : BaseEntity
{
    public AreaListModel()
    {
        Areas = new List<AreaModel>();
    }

    public IList<AreaModel> Areas { get; set; }
}
