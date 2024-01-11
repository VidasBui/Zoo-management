using System.ComponentModel.DataAnnotations;

namespace Zoo_management.Data.Entities
{
    public class EnclosureObject
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid EnclosureId { get; set; }
        public Enclosure Enclosure { get; set; }
    }
}
