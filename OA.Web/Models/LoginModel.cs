using OA.Core;
using System.ComponentModel.DataAnnotations;

namespace OA_WEB.Models
{
    public class LoginModel : BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool IsRemember { get; set; }
    }
}
