using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Core.Domain
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        //public string Address { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }


        #region Navigation Properties

        [ForeignKey("StateId")]
        public State State { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ICollection<Order> Orders { get; set; }

        #endregion
    }
}
