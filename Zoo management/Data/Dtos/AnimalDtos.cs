
namespace Zoo_management.Data.Dtos
{
    public record AnimalCreateDto(string species, string food, short amount);
    public record AnimalDto(Guid id, string species, string food, short amount, Guid? enclosureId);

    public record UploadAnimalsDto(AnimalCreateDto[] animals);
}
