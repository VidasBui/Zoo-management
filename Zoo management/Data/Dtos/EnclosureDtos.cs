
namespace Zoo_management.Data.Dtos
{
    public record EnclosureCreateDto(string name, string size, string location, string[] objects);
    public record EnclosureDto(Guid id, string name, string size, string location, short assignedAnimalCount, bool availableForCarnivores, bool availableForHerbivores, EnclosureObjectDto[] objects, AnimalDto[] animals);

    public record UploadEnclosuresDto(EnclosureCreateDto[] enclosures);
}
