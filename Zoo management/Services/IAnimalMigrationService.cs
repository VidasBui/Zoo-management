using Zoo_management.Data.Dtos;

namespace Zoo_management.Services
{
    public interface IAnimalMigrationService
    {
        Task<AnimalDto?> AddAnimal(AnimalCreateDto dto);
        Task<bool> RemoveAnimal(Guid animalId);
    }
}
