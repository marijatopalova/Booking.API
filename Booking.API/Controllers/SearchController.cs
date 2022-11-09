using Booking.API.DTOs.Search;
using Booking.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly IManager manager;

        public SearchController(IManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public async Task<ActionResult<SearchRes>> FilterOptionsAsync(SearchReq req)
        {
            if (string.IsNullOrEmpty(req.Destination) || req.FromDate == null || req.ToDate == null)
            {
                throw new ApplicationException("Required fields must be populated.");
            }

            SearchRes response = await manager.SearchAsync(req);

            return Ok(response);
        }
    }
}
