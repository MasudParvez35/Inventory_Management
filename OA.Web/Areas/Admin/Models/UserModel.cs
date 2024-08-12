using OA.Core;

namespace OA_WEB.Areas.Admin.Models
{
    public class UserModel : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }
}
