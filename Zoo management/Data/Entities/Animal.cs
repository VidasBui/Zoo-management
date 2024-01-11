using System.ComponentModel.DataAnnotations;
using Zoo_management.Data.ConstantsAndEnums;

namespace Zoo_management.Data.Entities
{
    public class Animal
    {
        [Required]
        [StringLength(60)]
        public string Species { get; set; }
        public FoodTypes Food { get; set; }
        public short Amount { get; set; }

        [Key]
        public Guid Id { get; set; }
        public Guid EnclosureId { get; set; }
        public Enclosure Enclosure { get; set; }
    }
}
