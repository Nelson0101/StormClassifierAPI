using Backend.Application;
using Backend.Infrastructure.Services;
using Backend.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation
{
    /// <summary>
    /// Entrance point of the Endpoint. 
    /// </summary>
    /// <param name="damageClassificationService"></param>
    [ApiController]
    [Route("api/[controller]/")]
    public class DamageController(DamageClassificationService damageClassificationService) : ControllerBase
    {
        [HttpGet("{location}/{date}/{mainProcess}")]
        public async Task<IActionResult> GetDamage([FromRoute] string location, [FromRoute] DateTime date, [FromRoute] string mainProcess)
        {
            var result = await damageClassificationService.ProcessDamageRequest(location, date, mainProcess);

            return result switch
            {
                ClassificationDto classification => Ok(classification),
                ErrorDto error => BadRequest(error),
                _ => StatusCode(500, new ErrorDto("Unexpected error occurred."))
            };
        }

    }
}