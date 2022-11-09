using Booking.API.DTOs.Book;
using Booking.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IManager manager;

        public BookController(IManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        public async Task<ActionResult<BookRes>> InsertBookingAsync(BookReq req)
        {
            BookRes bookRes = await manager.BookAsync(req);

            return Ok(bookRes);
        }
    }
}
