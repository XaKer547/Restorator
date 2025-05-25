using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorator.Domain.Models;
using Restorator.Domain.Models.Restaurant;
using Restorator.Domain.Models.Templates;
using Restorator.Domain.Services;

namespace Restorator.API.Controllers
{
    /// <summary>
    /// Контроллер управления ресторанами
    /// </summary>
    [ApiController, Route("api/[Controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        /// <inheritdoc/>
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        /// <summary>
        /// Поиск ресторанов
        /// </summary>
        /// <param name="name">Имя ресторана</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("search")]
        [ProducesResponseType<IReadOnlyCollection<RestaurantSearchItemDTO>>(200)]
        public async Task<IActionResult> SearchRestaurants([FromQuery] string name, CancellationToken cancellationToken)
        {
            var names = await _restaurantService.SearchRestaurants(name, cancellationToken);

            return Ok(names);
        }


        /// <summary>
        /// Получить полсдение посещеные рестораны
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType<IReadOnlyCollection<RestaurantPreviewDTO>>(200)]
        [ProducesResponseType(401)]
        [HttpGet("latest"), Authorize(Roles = "User")]
        public async Task<IActionResult> SearchRestaurants()
        {
            var restaurants = await _restaurantService.GetLatestVisited();

            return Ok(restaurants);
        }


        /// <summary>
        /// Получить информацию о ресторане
        /// </summary>
        /// <param name="restaurantId">Идентификатор ресторана</param>
        /// <returns></returns>
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


        /// <summary>
        /// Получить шаблоны ресторанов
        /// </summary>
        /// <returns></returns>
        [HttpGet("templates")]
        [ProducesResponseType<IReadOnlyCollection<RestaurantTemplateDTO>>(200)]
        public async Task<IActionResult> GetRestaurantTemplates()
        {
            var templates = await _restaurantService.GetRestaurantTemplates();

            return Ok(templates);
        }


        /// <summary>
        /// Создать ресторан
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Получить доступные теги ресторана
        /// </summary>
        /// <returns></returns>
        [HttpGet("tags")]
        [ProducesResponseType<IReadOnlyCollection<RestaurantTagDTO>>(200)]
        public async Task<IActionResult> GetRestaurantsTags()
        {
            var tags = await _restaurantService.GetRestaurantsTags();

            return Ok(tags);
        }


        /// <summary>
        /// Получить свои рестораны
        /// </summary>
        /// <returns></returns>
        [HttpGet("owned"), Authorize(Roles = "Manager")]
        [ProducesResponseType<IReadOnlyCollection<RestaurantPreviewDTO>>(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOwnedRestaurantPreviews()
        {
            var restaurants = await _restaurantService.GetOwnedRestaurantPreviews();

            return Ok(restaurants);
        }


        /// <summary>
        /// Получить поисковые значения свои рестораны
        /// </summary>
        /// <returns></returns>
        [HttpGet("owned/search"), Authorize(Roles = "Manager")]
        [ProducesResponseType<IReadOnlyCollection<RestaurantSearchItemDTO>>(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOwnedRestaurantSearchValues()
        {
            var restaurants = await _restaurantService.GetOwnedRestaurantsSearchItems();

            return Ok(restaurants);
        }


        /// <summary>
        /// Получить рестораны
        /// </summary>
        /// <param name="paginationFilter">Пагинация</param>
        /// <param name="filter">Фильтр поиска</param>
        /// <returns></returns>
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


        /// <summary>
        /// Изменить статус ресторана
        /// </summary>
        /// <param name="restaurantId">Идентификатор ресторана</param>
        /// <param name="approval">Подтверждение ресторана</param>
        /// <returns></returns>
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


        /// <summary>
        /// Удалить ресторан
        /// </summary>
        /// <param name="restaurantId">Идентификатор ресторана</param>
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


        /// <summary>
        /// Обновить информацию о ресторане
        /// </summary>
        /// <param name="model"></param>
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