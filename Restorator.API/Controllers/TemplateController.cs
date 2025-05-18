using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorator.Domain.Models.Templates;
using Restorator.Domain.Services;

namespace Restorator.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("/api/[controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpPost("table")]
        public async Task<IActionResult> CreateTableTemplate(CreateTableTempateDTO model)
        {
            var result = await _templateService.CreateTableTemplate(model);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost("restaurant")]
        public async Task<IActionResult> CreateRestaurantTemplate(CreateRestaurantTemplateDTO model)
        {
            var result = await _templateService.CreateRestaurantTemplate(model);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPut("restaurant")]
        public async Task<IActionResult> UpdateRestaurantTemplate(UpdateRestaurantTemplateDTO model)
        {
            var result = await _templateService.UpdateRestaurantTemplate(model);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("restaurant/{restaurantTemplateId:int}")]
        public async Task<IActionResult> DeleteRestaurantTemplate(int restaurantTemplateId)
        {
            var result = await _templateService.DeleteRestaurantTemplate(restaurantTemplateId);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }

        [HttpGet("restaurant")]
        public async Task<IActionResult> GetRestaurantsTemplatePreview()
        {
            var templates = await _templateService.GetRestaurantsTemplatePreview();

            return Ok(templates);
        }

        [HttpGet("restaurant/{restaurantTemplateId:int}")]
        public async Task<IActionResult> GetRestaurantsTemplatePreview(int restaurantTemplateId)
        {
            var templates = await _templateService.GetRestaurantTemplate(restaurantTemplateId);

            return Ok(templates);
        }

        [HttpGet("tables")]
        public async Task<IActionResult> GetTableTemplates()
        {
            var templates = await _templateService.GetTableTemplates();

            return Ok(templates);
        }
    }
}
