using Microsoft.AspNetCore.Mvc;
using Zoo_management.Data.Dtos;
using Zoo_management.Services;
using Zoo_management.Utils;

namespace Zoo_management.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalMigrationService _animalMigrationService;
        public AnimalsController(IAnimalMigrationService animalMigrationService)
        {
            _animalMigrationService = animalMigrationService;
        }

        [HttpPost]
        public async Task<ActionResult<AnimalDto>> Create (AnimalCreateDto dto)
        {
            return await JobHandlingUtils.HandleJobAsync(async () =>
            {
                var res = await _animalMigrationService.AddAnimal(dto);
                if (res == null)
                {
                    return BadRequest();
                }
                return Created("", res);
            });
        }

        [HttpDelete("{animalGuid}")]
        public async Task<IActionResult> Remove (Guid animalGuid)
        {
            return await JobHandlingUtils.HandleJobAsync(async () =>
            {
                var res = await _animalMigrationService.RemoveAnimal(animalGuid);
                if (!res) return NotFound();
                return NoContent();
            });
        }
    }
}
