using BusinessLogic.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }


        [HttpPost]
        public async Task<ActionResult> CreateAsync(ReservationDto reservationDto)
        {
            var result = await _reservationService.CreateAsync(reservationDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }


            return Ok(result.Data);
        }
    }
}
