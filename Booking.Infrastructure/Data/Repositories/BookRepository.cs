using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext context;

        public BookRepository(BookDbContext context)
        {
            this.context = context;
        }

        public async Task BookAsync(Book book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();
        }

        public async Task<Book?> GetBookingByCodeAsync(string code)
        {
            return await context.Books.FirstOrDefaultAsync(x => x.BookingCode == code);
        }
    }
}
