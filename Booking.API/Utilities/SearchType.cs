using Booking.API.DTOs.Search;
using Booking.Domain.Common;

namespace Booking.API.Utilities
{
    public static class SearchType
    {
        public static SearchTypeEnum GetSearchType(SearchReq req)
        {
            if (string.IsNullOrEmpty(req.DepartureAirport))
            {
                return SearchTypeEnum.HotelOnly;
            }
            else if (req.FromDate >= DateTime.Today && req.FromDate < DateTime.Today.AddDays(45))
            {
                return SearchTypeEnum.LastMinuteHotels;
            }
            else
            {
                return SearchTypeEnum.HotelAndFlight;
            }
        }
    }
}
