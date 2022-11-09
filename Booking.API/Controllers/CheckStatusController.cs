using Booking.API.DTOs.CheckStatus;
using Booking.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckStatusController : Controller
    {
        private readonly IManager manager;

        public CheckStatusController(IManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public async Task<ActionResult<CheckStatusRes>> GetStatusAsync(CheckStatusReq req)
        {
            CheckStatusRes checkStatusRes = await manager.CheckStatusAsync(req);

            return Ok(checkStatusRes);
        }
    }
}
