using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorator.API.Extensions;
using Restorator.Domain.Models;
using Restorator.Domain.Services;

namespace Restorator.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("plan")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetRestaurantPlan([FromQuery] GetRestaurantPlanDTO model)
        {
            if (!User.TryGetUserId(out var userId))
                return BadRequest();

            var result = await _reservationService.GetRestaurantPlan(userId, model);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ReserveTables(CreateRestaurantReservationDTO model)
        {
            if (!User.TryGetUserId(out var userId))
                return BadRequest();

            var result = await _reservationService.CreateReservation(userId, model);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost("cancel")]
        [Authorize(Roles = "User,Manager")]
        public async Task<IActionResult> CancelReservation(CancelReservationDTO model)
        {
            if (!User.TryGetUserId(out var userId))
                return BadRequest();

            var result = await _reservationService.CancelReservation(userId, model);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetReservation(GetReservationInfoDTO model)
        {
            if (!User.TryGetUserId(out var userId))
                return BadRequest();

            var result = await _reservationService.GetReservation(userId, model);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetReservations(GetReservationsDTO model)
        {
            if (!User.TryGetUserId(out var userId))
                return BadRequest();

            var result = await _reservationService.GetReservations(userId, model);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
