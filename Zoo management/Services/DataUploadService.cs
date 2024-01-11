using Zoo_management.Data.ConstantsAndEnums;
using Zoo_management.Data.Converters;
using Zoo_management.Data.Dtos;
using Zoo_management.Data.Entities;
using Zoo_management.Data.Repositories;

namespace Zoo_management.Services
{
    public class DataUploadService : IDataUploadService
    {
        private readonly IEnclosuresRepository _enclosuresRepository;
        private readonly IEnclosureObjectsRepository _enclosureObjectsRepository;
        private readonly IAnimalMigrationService _animalMigrationService;
        public DataUploadService(IEnclosuresRepository enclosuresRepository, IEnclosureObjectsRepository enclosureObjectsRepository, IAnimalMigrationService animalMigrationService)
        {
            _enclosuresRepository = enclosuresRepository;
            _enclosureObjectsRepository = enclosureObjectsRepository;
            _animalMigrationService = animalMigrationService;
        }

        public async Task<List<EnclosureDto>?> UploadEnclosures(IReadOnlyList<EnclosureCreateDto> dtos)
        {
            var res = new List<EnclosureDto>();
            foreach (var dto in dtos)
            {
                var enclosure = dto.ToEntity();
                if (enclosure == null) return null;

                await _enclosuresRepository.CreateAsync(enclosure);
                var objects = new List<EnclosureObject>();
                foreach (var name in dto.objects)
                {
                    var obj = name.ToEntity(enclosure.Id);
                    await _enclosureObjectsRepository.CreateAsync(obj);
                    objects.Add(obj);
                }
                res.Add(enclosure.ToDto(objects));

            }
            return res;
        }

        public async Task<List<AnimalDto>> UploadAnimals(IReadOnlyList<AnimalCreateDto> dtos)
        {
            var res = new List<AnimalDto>();

            var carnivoreDtos = dtos
                .Where(x => x.food == FoodTypes.Carnivore.ToString())
                .OrderByDescending(x => x.amount).ToArray();

            var otherAnimalDtos = dtos
                .Where(x => x.food != FoodTypes.Carnivore.ToString())
                .OrderByDescending(x => x.amount).ToArray();

            var enclosures = await _enclosuresRepository.GetManyAsync();
            foreach(var dto in carnivoreDtos)
            {
                var assignedAnimal = await _animalMigrationService.AddAnimal(dto);
                if (assignedAnimal != null) res.Add(assignedAnimal);
            }
            foreach (var dto in otherAnimalDtos)
            {
                var assignedAnimal = await _animalMigrationService.AddAnimal(dto);
                if (assignedAnimal != null) res.Add(assignedAnimal);
            }

            return res;
        }
    }
}
