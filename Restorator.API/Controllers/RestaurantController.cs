using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorator.Domain.Models;
using Restorator.Domain.Models.Restaurant;
using Restorator.Domain.Services;

namespace Restorator.API.Controllers
{
    [ApiController, Route("api/[Controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("search")]
        [ProducesResponseType<IReadOnlyCollection<RestaurantSearchItemDTO>>(200)]
        public async Task<IActionResult> SearchRestaurants([FromQuery] string name, CancellationToken cancellationToken)
        {
            var names = await _restaurantService.SearchRestaurants(name, cancellationToken);

            return Ok(names);
        }


        [HttpGet("{restaurantId:int}")]
        [ProducesResponseType<RestaurantInfoDTO>(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRestaurantInfo(int restaurantId)
        {
            var result = await _restaurantService.GetRestaurantInfo(restaurantId);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }


        [HttpGet("templates")]
        [ProducesResponseType<IReadOnlyCollection<RestaurantTemplateDTO>>(200)]
        public async Task<IActionResult> GetRestaurantTemplates()
        {
            var templates = await _restaurantService.GetRestaurantTemplates();

            return Ok(templates);
        }


        [HttpPost, Authorize(Roles = "Manager")]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantDTO model)
        {
            var result = await _restaurantService.CreateRestaurant(model);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }


        [HttpGet("tags")]
        [ProducesResponseType<IReadOnlyCollection<RestaurantTagDTO>>(200)]
        public async Task<IActionResult> GetRestaurantsTags()
        {
            var tags = await _restaurantService.GetRestaurantsTags();

            return Ok(tags);
        }


        [HttpGet("owned"), Authorize(Roles = "Manager")]
        [ProducesResponseType<IReadOnlyCollection<RestaurantPreviewDTO>>(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOwnedRestaurantPreviews()
        {
            var restaurants = await _restaurantService.GetOwnedRestaurantPreviews();

            return Ok(restaurants);
        }

        [HttpGet]
        [ProducesResponseType<IReadOnlyCollection<RestaurantPreviewDTO>>(200)]
        public async Task<IActionResult> GetRestaurantPreviews([FromQuery] PaginationFilter paginationFilter, [FromQuery] GetRestaurantsPreviewFilter filter)
        {
            var restaurants = await _restaurantService.GetRestaurantPreviews(new GetRestaurantsPreviewDTO()
            {
                Filter = filter,
                PaginationFilter = paginationFilter
            });

            return Ok(restaurants);
        }

        [HttpPatch("{restaurantId:int}/approve"), Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ChangeRestaurantApproval(int restaurantId, [FromBody] bool approval)
        {
            var result = await _restaurantService.ChangeRestaurantApproval(new ChangeRestaurantApprovalDTO()
            {
                RestaurantId = restaurantId,
                Approval = approval
            });

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{restaurantId:int}"), Authorize(Roles = "Manager")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId)
        {
            var result = await _restaurantService.DeleteRestaurant(restaurantId);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }

        [HttpPut, Authorize(Roles = "Manager")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateRestaurant(UpdateRestraurantDTO model)
        {
            var result = await _restaurantService.UpdateRestaurant(model);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }
    }
}