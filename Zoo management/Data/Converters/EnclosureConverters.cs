using Zoo_management.Data.ConstantsAndEnums;
using Zoo_management.Data.Dtos;
using Zoo_management.Data.Entities;

namespace Zoo_management.Data.Converters
{
    public static class EnclosureConverters
    {
        public static short ToNumber (this EnclosureSize val) 
        {
            switch (val) 
            {
                case EnclosureSize.Small:
                    return 3;
                case EnclosureSize.Medium:
                    return 5;
                case EnclosureSize.Large:
                    return 8;
                case EnclosureSize.Huge:
                    return 15;
                default: return 0;
            }
        }

        public static Enclosure? ToEntity (this EnclosureCreateDto dto)
        {
            if (Enum.TryParse(dto.size, out EnclosureSize sizeEnum) 
                && Enum.TryParse(dto.location, out EnclosureLocation locationEnum))
            {
                return new Enclosure()
                {
                    Name = dto.name,
                    Size = sizeEnum,
                    Location = locationEnum,
                    assignedAnimalCount = 0,
                    Id = Guid.NewGuid()
                };
            }
            return null;
        }

        public static EnclosureDto ToDto (this Enclosure e, IReadOnlyList<EnclosureObject>? objects = null, IReadOnlyList<Animal>? animals = null)
        {
            objects = objects ?? new List<EnclosureObject>();
            animals = animals ?? new List<Animal>();

            return new EnclosureDto(
                e.Id,
                e.Name,
                e.Size.ToString(),
                e.Location.ToString(),
                e.assignedAnimalCount,
                e.availableForCarnivores,
                e.availableForHerbivores,
                objects.Select(x => x.ToDto()).ToArray(),
                animals.Select(x => x.ToDto()).ToArray()
           );
        }
    }
}
