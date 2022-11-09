using Booking.API.DTOs.Book;
using Booking.API.DTOs.CheckStatus;
using Booking.API.DTOs.Search;
using Booking.API.Utilities;
using Booking.Domain.Common;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;

namespace Booking.API.Services
{
    public class Manager : IManager
    {
        private readonly IBookRepository bookRepository;
        private readonly ISearchRepository searchRepository;

        public Manager(IBookRepository bookRepository, ISearchRepository searchRepository)
        {
            this.bookRepository = bookRepository;
            this.searchRepository = searchRepository;
        }

        public async Task<BookRes> BookAsync(BookReq req)
        {
            Option option = await searchRepository.GetOptionByCodeAsync(req.OptionCode);

            if (option == null)
            {
                throw new ApplicationException("The option you are trying to book was not found.");
            }

            var newBooking = new Book()
            {
                BookingCode = RandomGenerator.RandomString(6),
                BookingTime = DateTime.UtcNow,
                SleepTime = RandomGenerator.RandomNumber(30, 60),
                SearchType = SearchType.GetSearchType(req.SearchRequest)
            };

            await bookRepository.BookAsync(newBooking);

            return new BookRes
            {
                BookingCode = newBooking.BookingCode,
                BookingTime = newBooking.BookingTime
            };
        }

        public async Task<CheckStatusRes> CheckStatusAsync(CheckStatusReq req)
        {
            Book? book = await bookRepository.GetBookingByCodeAsync(req.BookingCode);

            if (book == null)
            {
                throw new ApplicationException("Booking not found");
            }

            CheckStatusRes response = new();

            if (DateTime.UtcNow < book.BookingTime.AddSeconds(book.SleepTime))
            {
                response.Status = BookingStatusEnum.Pending.ToString();
            }
            else if (DateTime.UtcNow >= book.BookingTime.AddSeconds(book.SleepTime))
            {
                response.Status = book.SearchType == SearchTypeEnum.LastMinuteHotels
                    ? BookingStatusEnum.Failed.ToString()
                    : BookingStatusEnum.Success.ToString();
            }

            return response;
        }

        public async Task<SearchRes> SearchAsync(SearchReq req)
        {
            var optionsList = await searchRepository.GetOptionsAsync(req.Destination);

            var searchType = SearchType.GetSearchType(req);

            if (searchType == SearchTypeEnum.HotelOnly || searchType == SearchTypeEnum.LastMinuteHotels)
            {
                optionsList.ForEach(o => o.FlightCode = String.Empty);
            }

            return new SearchRes()
            {
                Options = optionsList
            };
        }
    }
}
