using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task BookAsync(Book booking);
        Task<Book?> GetBookingByCodeAsync(string code);
    }
}
