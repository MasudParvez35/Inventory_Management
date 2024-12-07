using OA.Core;

namespace OA_WEB.Areas.Admin.Models;

public class AreaModel : BaseEntity
{
    public string Name { get; set; }

    public int DisplayOrder { get; set; }
}
