using Zoo_management.Data.ConstantsAndEnums;
using Zoo_management.Data.Converters;
using Zoo_management.Data.Dtos;
using Zoo_management.Data.Entities;
using Zoo_management.Data.Repositories;

namespace Zoo_management.Services
{
    public class AnimalMigrationService : IAnimalMigrationService
    {
        private readonly IAnimalsRepository _animalsRepository;
        private readonly IEnclosuresRepository _enclosuresRepository;
        public AnimalMigrationService(IAnimalsRepository animalsRepository, IEnclosuresRepository enclosuresRepository)
        {
            _animalsRepository = animalsRepository;
            _enclosuresRepository = enclosuresRepository;
        }

        public async Task<bool> RemoveAnimal(Guid animalId) 
        {
            var animal = await _animalsRepository.GetAsync(animalId);
            if (animal == null) return false;
            var enclosure = await _enclosuresRepository.GetAsync(animal.EnclosureId);
            
            if(enclosure == null) return false;
            enclosure.assignedAnimalCount -= animal.Amount;

            if(enclosure.assignedAnimalCount == 0) 
            {
                enclosure.availableForCarnivores = true;
                enclosure.availableForHerbivores = true;
            }
            else if(animal.Food == FoodTypes.Carnivore)
            {
                enclosure.availableForCarnivores = true;
            }
            await _enclosuresRepository.UpdateAsync(enclosure);
            await _animalsRepository.DeleteAsync(animal);
            
            return true;
        }

        public async Task<AnimalDto?> AddAnimal(AnimalCreateDto dto) 
        {
            var allAnimals = await _animalsRepository.GetManyAsync();
            var sameSpecie = allAnimals.FirstOrDefault(x => x.Species == dto.species);
            if(sameSpecie != null)
            {
                var enclosure = await _enclosuresRepository.GetAsync(sameSpecie.EnclosureId);
                if (enclosure == null) return null;

                return await joinAnimals(enclosure, sameSpecie, dto.amount);
            }
            var enclosures = await _enclosuresRepository.GetManyAsync();
            if(dto.food == FoodTypes.Carnivore.ToString()) 
            {
                var suitableEnclosures = enclosures.Where(x => x.availableForCarnivores);
                var emptyEnclosures = suitableEnclosures.Where(x => x.assignedAnimalCount == 0);
                if (emptyEnclosures.Count() > 1) //leave at least one space for herbivores
                {
                    return await assignAnimal(emptyEnclosures, dto, true);
                }
                else
                {
                    var occupiedSuitableEnclosures = enclosures.Where(x => x.availableForCarnivores && x.assignedAnimalCount > 0);
                    return await assignAnimal(occupiedSuitableEnclosures, dto, true);
                }
            }
            else 
            {
                var suitableEnclosures = enclosures.Where(x => x.availableForHerbivores);
                return await assignAnimal(suitableEnclosures, dto, false);
            }
        }

        private async Task<AnimalDto> joinAnimals(Enclosure enclosure, Animal animal, short incomingAmount) 
        {
            animal.Amount += incomingAmount;
            await _animalsRepository.UpdateAsync(animal);
            enclosure.assignedAnimalCount += incomingAmount;
            await _enclosuresRepository.UpdateAsync(enclosure);
            return animal.ToDto();
        }

        private async Task<AnimalDto?> assignAnimal(IEnumerable<Enclosure> suitableEnclosures, AnimalCreateDto dto, bool isCarnivore)
        {
            var enc = getMostSuitableEnclosure(suitableEnclosures, dto);
            var animal = dto.ToEntity(enc.Id);
            if (animal == null) return null;

            await _animalsRepository.CreateAsync(animal);
            if (isCarnivore)
            {
                enc.availableForHerbivores = false;
                enc.availableForCarnivores = enc.assignedAnimalCount > 0 ? false : true;
                enc.assignedAnimalCount += dto.amount;
            }
            else
            {
                enc.availableForCarnivores = false;
                enc.assignedAnimalCount += dto.amount;
            }
            await _enclosuresRepository.UpdateAsync(enc);
            return animal.ToDto();
        }

        private Enclosure getMostSuitableEnclosure(IEnumerable<Enclosure> enclosures, AnimalCreateDto dto)
        {
            (double closestToZero, Enclosure? enclosure) = (double.MaxValue, null);
            foreach (var e in enclosures)
            {
                var score = Math.Abs(e.availabilityIndex - ((double)dto.amount / e.Size.ToNumber()));
                if (score < closestToZero)
                {
                    closestToZero = score;
                    enclosure = e;
                }
            }
            if (enclosure == null) throw new Exception($"Enclosure for {dto.species} not found");
            return enclosure;
        }
    }
}
