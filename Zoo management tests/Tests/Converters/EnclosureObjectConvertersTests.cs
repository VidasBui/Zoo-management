using Zoo_management.Data.Converters;
using Zoo_management.Data.Dtos;
using Zoo_management.Data.Entities;

namespace Zoo_management_tests.Tests.Converters
{
    public class EnclosureObjectConvertersTests
    {
        [Fact]
        public void ConvertFromDtoToEntityTest()
        {
            var enclosureId = Guid.NewGuid();

            for (int i = 0; i < 5; i++)
            {
                var dto = $"object {i}";
                var entity = dto.ToEntity(enclosureId);
                entity.Name.Should().Be($"object {i}");
                entity.EnclosureId.Should().Be(enclosureId);
                entity.Id.Should().NotBe(Guid.Empty);
            }
        }

        [Fact]
        public void ConvertFromEntityToDtoTest()
        {
            for (int i = 0; i < 5; i++)
            {
                var guid = Guid.NewGuid();
                var enclosureId = Guid.NewGuid();
                var entity = new EnclosureObject()
                {
                    Id = guid,
                    Name = $"object {i}",
                    EnclosureId = enclosureId
                };

                var dto = entity.ToDto();

                dto.id.Should().Be(guid);
                dto.name.Should().Be($"object {i}");
                dto.enclosureId.Should().Be(enclosureId);
            }
        }

    }
}
