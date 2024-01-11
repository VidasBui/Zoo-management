using Microsoft.EntityFrameworkCore.Storage;
using Zoo_management.Data.ConstantsAndEnums;
using Zoo_management.Data.Converters;
using Zoo_management.Data.Dtos;
using Zoo_management.Data.Entities;

namespace Zoo_management_tests.Tests.Converters
{
    public class EnclosureConvertersTests
    {
        [Fact]
        public void ConvertFromDtoToEntityTest()
        {
            Array enclosureSizeArray = Enum.GetValues(typeof(EnclosureSize));
            Array enclosureLocationArray = Enum.GetValues(typeof(EnclosureLocation));

            for (int i = 0; i < enclosureSizeArray.Length; i++)
            {
                for (int j = 0; j < enclosureLocationArray.Length; j++)
                {
                    EnclosureSize size = (EnclosureSize)enclosureSizeArray.GetValue(i)!;
                    EnclosureLocation location = (EnclosureLocation)enclosureLocationArray.GetValue(j)!;

                    var dto = new EnclosureCreateDto($"enclosure{i}", size.ToString(), location.ToString(), []);
                    var entity = dto.ToEntity();

                    entity.Should().NotBeNull();
                    entity.Name.Should().Be($"enclosure{i}");
                    entity.Size.Should().Be(size);
                    entity.Location.Should().Be(location);
                    entity.assignedAnimalCount.Should().Be(0);
                    entity.availableForCarnivores.Should().BeTrue();
                    entity.availableForHerbivores.Should().BeTrue();
                    entity.Id.Should().NotBe(Guid.Empty);
                }
            }

            var invalidDto = new EnclosureCreateDto("enclosure", "InvalidSize", "InvalidLocation", []);
            invalidDto.ToEntity().Should().BeNull();
        }

        [Fact]
        public void ConvertFromEntityToDtoTest()
        {
            Array enclosureSizeArray = Enum.GetValues(typeof(EnclosureSize));
            Array enclosureLocationArray = Enum.GetValues(typeof(EnclosureLocation));

            for (int i = 0; i < enclosureSizeArray.Length; i++)
            {
                for (int j = 0; j < enclosureLocationArray.Length; j++)
                {
                    var guid = Guid.NewGuid();
                    EnclosureSize size = (EnclosureSize)enclosureSizeArray.GetValue(i)!;
                    EnclosureLocation location = (EnclosureLocation)enclosureLocationArray.GetValue(j)!;

                    var entity = new Enclosure()
                    {
                        Id = guid,
                        Name = $"enclosure {i}",
                        Size = size,
                        Location = location,
                        assignedAnimalCount = (short)(i + 4)
                    };
                    var objects = new List<EnclosureObject>();
                    var animals = new List<Animal>();
                    for (int k = 0; k < 3; k++)
                    {
                        var dto = entity.ToDto(objects, animals);
                        dto.id.Should().Be(guid);
                        dto.name.Should().Be($"enclosure {i}");
                        dto.size.Should().Be(size.ToString());
                        dto.location.Should().Be(location.ToString());
                        dto.assignedAnimalCount.Should().Be((short)(i + 4));
                        dto.availableForCarnivores.Should().BeTrue();
                        dto.availableForHerbivores.Should().BeTrue();
                        dto.objects.Should().BeEquivalentTo(objects.Select(x => x.ToDto()).ToList());
                        dto.animals.Should().BeEquivalentTo(animals.Select(x => x.ToDto()).ToList());
                        objects.Add(new EnclosureObject()
                        {
                            Name = $"object {k}",
                            Id = Guid.NewGuid()
                        });
                        animals.Add(new Animal()
                        {
                            Species = $"specie {i}",
                            Food = FoodTypes.Carnivore,
                            Amount = (short)(k + 6),
                            Id = Guid.NewGuid(),
                            EnclosureId = Guid.NewGuid()
                        });
                    }
                }
            }
        }

    }
}
