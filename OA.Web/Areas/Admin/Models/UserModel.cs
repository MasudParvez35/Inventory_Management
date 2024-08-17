using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OA.Core;

namespace OA_WEB.Areas.Admin.Models
{
    public class UserModel : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }


        #region Navigation Properties

        [ValidateNever]
        public string StateName { get; set; }

        [ValidateNever]
        public string CityName { get; set; }

        #endregion
    }
}
