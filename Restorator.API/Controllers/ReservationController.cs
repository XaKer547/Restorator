using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorator.Domain.Models.Reservations;
using Restorator.Domain.Models.Restaurant;
using Restorator.Domain.Services;

namespace Restorator.API.Controllers
{
    [Authorize]
    [ApiController, Route("api/[Controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }


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


        [HttpGet("filter"), Authorize(Roles = "User")]
        [ProducesResponseType<IReadOnlyCollection<ReservationInfoDTO>>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetReservations([FromQuery] GetReservationsDTO model)
        {
            var result = await _reservationService.GetReservations(model);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }


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
