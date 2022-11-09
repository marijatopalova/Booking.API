using Booking.API.DTOs.Search;

namespace Booking.API.DTOs.Book
{
    public class BookReq
    {
        public string OptionCode { get; set; }
        public SearchReq SearchRequest { get; set; }
    }
}
