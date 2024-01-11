using Zoo_management.Data.Dtos;
using Zoo_management.Data.Entities;

namespace Zoo_management.Data.Converters
{
    public static class EnclosureObjectConverters
    {
        public static EnclosureObject ToEntity (this string enclosureObjectName, Guid enclosureId)
        {
            return new EnclosureObject()
            {
                Name = enclosureObjectName,
                EnclosureId = enclosureId,
                Id = Guid.NewGuid()
            };
        }

        public static EnclosureObjectDto ToDto (this EnclosureObject e)
        {
            return new EnclosureObjectDto(e.Id, e.Name, e.EnclosureId);
        }

    }
}
