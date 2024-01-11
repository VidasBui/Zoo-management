using Microsoft.AspNetCore.Mvc;
using Zoo_management.Data.Dtos;
using Zoo_management.Services;
using Zoo_management.Utils;

namespace Zoo_management.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class DataUploadController : ControllerBase
    {
        private readonly IDataUploadService _dataUploadService;
        public DataUploadController(IDataUploadService dataUploadService)
        {
            _dataUploadService = dataUploadService;
        }

        [HttpPost("enclosures")]
        public async Task<ActionResult<EnclosureDto[]>> UploadEnclosures(UploadEnclosuresDto dtos)
        {
            return await JobHandlingUtils.HandleJobAsync(async () =>
            {
                var res = await _dataUploadService.UploadEnclosures(dtos.enclosures);
                if (res == null)
                {
                    return BadRequest();
                }
                return Created("", res);
            });
        }

        [HttpPost("animals")]
        public async Task<IActionResult> UploadAnimals(UploadAnimalsDto dtos)
        {
            return await JobHandlingUtils.HandleJobAsync(async () =>
            {
                var res = await _dataUploadService.UploadAnimals(dtos.animals);
                if (res == null)
                {
                    return BadRequest();
                }
                return Created("", res);
            });
        }
    }
}
