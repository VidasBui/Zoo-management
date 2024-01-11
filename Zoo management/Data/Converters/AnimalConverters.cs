using Zoo_management.Data.ConstantsAndEnums;
using Zoo_management.Data.Dtos;
using Zoo_management.Data.Entities;

namespace Zoo_management.Data.Converters
{
    public static class AnimalConverters
    {

        public static Animal? ToEntity(this AnimalCreateDto dto, Guid enclosureId) 
        {
            if (Enum.TryParse(dto.food, out FoodTypes foodEnum))
            {
                return new Animal()
                {
                    Species = dto.species,
                    Food = foodEnum,
                    Amount = dto.amount,
                    EnclosureId = enclosureId,
                    Id = Guid.NewGuid()
                };
            }
            return null;
        }

        public static AnimalDto ToDto(this Animal e)
        {
            return new AnimalDto(e.Id, e.Species, e.Food.ToString(), e.Amount, e.EnclosureId);
        }

    }
}
