using Booking.API.DTOs.Book;
using Booking.API.DTOs.CheckStatus;
using Booking.API.DTOs.Search;

namespace Booking.API.Services
{
    public interface IManager
    {
        Task<BookRes> BookAsync(BookReq req);
        Task<SearchRes> SearchAsync(SearchReq req);
        Task<CheckStatusRes> CheckStatusAsync(CheckStatusReq req);
    }
}
