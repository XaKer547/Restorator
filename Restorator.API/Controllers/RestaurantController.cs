using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorator.Domain.Models;
using Restorator.Domain.Models.Restaurant;
using Restorator.Domain.Services;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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


        [HttpGet("names")]
        public async Task<IActionResult> GetRestaurantNames()
        {
            var names = await _restaurantService.GetRestaurantNames();

            return Ok(names);
        }


        [HttpGet("{restaurantId:int}")]
        public async Task<IActionResult> GetRestaurantInfo(int restaurantId)
        {
            var info = await _restaurantService.GetRestaurantInfo(restaurantId);

            return Ok(info);
        }


        [HttpGet("templates")]
        public async Task<IActionResult> GetRestaurantTemplates()
        {
            var templates = await _restaurantService.GetRestaurantTemplates();

            return Ok(templates);
        }


        [HttpPost, Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantDTO model)
        {
            var result = await _restaurantService.CreateRestaurant(model);

            if (result.IsFailed)
                return BadRequest();

            return Ok(result.Value);
        }


        [HttpGet("tags")]
        public async Task<IActionResult> GetRestaurantsTags()
        {
            var tags = await _restaurantService.GetRestaurantsTags();

            return Ok(tags);
        }


        [HttpGet("owned"), Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetOwnedRestaurantPreviews()
        {
            var restaurants = await _restaurantService.GetOwnedRestaurantPreviews();

            return Ok(restaurants);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetRestaurantPreviews([FromQuery] PaginationFilter paginationFilter, [FromQuery] GetRestaurantsPreviewFilter filter)
        {
            var restaurants = await _restaurantService.GetRestaurantPreviews(new GetRestaurantsPreviewDTO()
            {
                Filter = filter,
                PaginationFilter = paginationFilter
            });

            return Ok(restaurants);
        }

        [HttpPatch("{restaurantId:int}/approve")]
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

        [HttpDelete("{restaurantId:int}")]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId)
        {
            var result = await _restaurantService.DeleteRestaurant(restaurantId);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRestaurant(UpdateRestraurantDTO model)
        {
            var result = await _restaurantService.UpdateRestaurant(model);

            if (result.IsFailed)
                return BadRequest();

            return Ok();
        }
    }
}