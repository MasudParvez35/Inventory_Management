using OA.Core;

namespace OA_WEB.Areas.Admin.Models;

public class StateListModel : BaseEntity
{
    public StateListModel()
    {
        States = new List<StateModel>();
    }

    public IList<StateModel> States { get; set; }
}
