using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Core.Domain
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int StateId { get; set; }
        [ForeignKey("StateId")]

        public State State { get; set; }
    }
}
