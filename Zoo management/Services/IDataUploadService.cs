using Zoo_management.Data.Dtos;

namespace Zoo_management.Services
{
    public interface IDataUploadService
    {
        Task<List<EnclosureDto>?> UploadEnclosures(IReadOnlyList<EnclosureCreateDto> dtos);
        Task<List<AnimalDto>> UploadAnimals(IReadOnlyList<AnimalCreateDto> dtos);
    }
}
