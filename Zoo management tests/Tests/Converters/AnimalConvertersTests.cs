using Zoo_management.Data.ConstantsAndEnums;
using Zoo_management.Data.Converters;
using Zoo_management.Data.Dtos;
using Zoo_management.Data.Entities;

namespace Zoo_management_tests.Tests.Converters
{
    public class AnimalConvertersTests
    {
        [Fact]
        public void ConvertFromDtoToEntityTest()
        {
            Array foodTypesArray = Enum.GetValues(typeof(FoodTypes));

            for (int i = 0; i < foodTypesArray.Length; i++)
            {
                FoodTypes type = (FoodTypes) foodTypesArray.GetValue(i)!;
                var dto = new AnimalCreateDto($"specie{i}", type.ToString(), (short) (i + 3));
                var guid = Guid.NewGuid();
                var entity = dto.ToEntity(guid)!;
                entity.Id.Should().NotBe(Guid.Empty);
                entity.Amount.Should().Be((short)(i + 3));
                entity.Food.Should().Be(type);
                entity.Species.Should().Be($"specie{i}");
                entity.EnclosureId.Should().Be(guid);
            };
            var invalidDto = new AnimalCreateDto("specie", "Invalid", 4);
            invalidDto.ToEntity(Guid.NewGuid()).Should().BeNull();
        }

        [Fact]
        public void ConvertFromEntityToDtoTest()
        {
            Array foodTypesArray = Enum.GetValues(typeof(FoodTypes));

            for (int i = 0; i < foodTypesArray.Length; i++)
            {
                var guid = Guid.NewGuid();
                var enclosureId = Guid.NewGuid();
                FoodTypes type = (FoodTypes)foodTypesArray.GetValue(i)!;
                var entity = new Animal()
                {
                    Id = guid,
                    Amount = (short)(i + 4),
                    Food = type,
                    Species = $"specie {i}",
                    EnclosureId = enclosureId
                };
                var dto = entity.ToDto();
                dto.id.Should().Be(guid);
                dto.amount.Should().Be((short)(i + 4));
                dto.food.Should().Be(type.ToString());
                dto.species.Should().Be($"specie {i}");
                dto.enclosureId.Should().Be(enclosureId);
            };
        }

    }
}
