using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorator.Domain.Services;
using System.ComponentModel.DataAnnotations;

namespace Restorator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthSummaryReport([FromQuery][Required] DateOnly selectedDate, [FromQuery] int? restaurantId = default)
        {
            var report = await _reportService.GetMonthSummaryReport(selectedDate, restaurantId);

            return Ok(report);
        }
    }
}
