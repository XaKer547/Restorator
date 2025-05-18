using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorator.Domain.Models.Reservations;
using Restorator.Domain.Models.Restaurant;
using Restorator.Domain.Services;
using System.ComponentModel.DataAnnotations;

namespace Restorator.API.Controllers
{
    /// <summary>
    /// Контроллер управления бронированием
    /// </summary>
    [Authorize]
    [ApiController, Route("api/[Controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        /// <inheritdoc/>
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Получить схему бронирования ресторана
        /// </summary>
        /// <param name="restaurantId">Идентификатор ресторана</param>
        /// <param name="reservationStartDate">Начало бронирования</param>
        /// <param name="reservationEndDate">Конец бронирования</param>

        [HttpGet("{restaurantId:int}/plan"), Authorize(Roles = "User")]
        [ProducesResponseType<RestaurantPlanDTO>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetRestaurantReservationPlan(int restaurantId, DateTime reservationStartDate, DateTime reservationEndDate)
        {
            var result = await _reservationService.GetRestaurantReservationPlan(new GetRestaurantPlanDTO()
            {
                RestaurantId = restaurantId,
                ReservationStartDate = reservationStartDate,
                ReservationEndDate = reservationEndDate
            });

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }


        /// <summary>
        /// Получить бронирования
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = "User,Manager")]
        [ProducesResponseType<IReadOnlyCollection<ReservationInfoDTO>>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetReservations([Required] DateOnly selectedDate, int restaurantId, int userId, bool skipCanceled)
        {
            var result = await _reservationService.GetReservations(new GetReservationsDTO()
            {
                RestaurantId = restaurantId,
                UserId = userId,
                SelectedDate = selectedDate,
                SkipCanceled = skipCanceled
            });

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }


        /// <summary>
        /// Получить сведения о бронировании
        /// </summary>
        /// <param name="reservationId">Идентификатор бронирования</param>
        /// <returns></returns>
        [HttpGet("{reservationId:int}"), Authorize(Roles = "User")]
        [ProducesResponseType<ReservationInfoDTO>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetReservation(int reservationId)
        {
            var result = await _reservationService.GetReservationInfo(reservationId);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }


        /// <summary>
        /// Создать бронь
        /// </summary>
        /// <param name="model"></param>
        [HttpPost, Authorize(Roles = "User")]
        [ProducesResponseType<int>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ReserveTables(CreateRestaurantReservationDTO model)
        {
            var result = await _reservationService.CreateReservation(model);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }


        /// <summary>
        /// Отменить бронь
        /// </summary>
        /// <param name="reservationId">Идентификатор бронирования</param>
        [HttpHead("{reservationId:int}/cancel"), Authorize(Roles = "User,Manager")]
        [ProducesResponseType<int>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            var result = await _reservationService.CancelReservation(reservationId);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }
}