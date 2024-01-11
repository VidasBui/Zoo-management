using System.ComponentModel.DataAnnotations;
using Zoo_management.Data.ConstantsAndEnums;
using Zoo_management.Data.Converters;

namespace Zoo_management.Data.Entities
{
    public class Enclosure
    {
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        [Required]
        public EnclosureSize Size { get; set; }
        [Required]
        public EnclosureLocation Location { get; set; }
        [Required]
        public short assignedAnimalCount { get; set; } = 0;
        [Required]
        public bool availableForHerbivores { get; set; } = true;
        [Required]
        public bool availableForCarnivores { get; set; } = true;
        [Required]
        public double availabilityIndex => (
            (Size.ToNumber() - assignedAnimalCount)
            / Size.ToNumber()
        );

        [Key]
        public Guid Id { get; set; }
    }
}
