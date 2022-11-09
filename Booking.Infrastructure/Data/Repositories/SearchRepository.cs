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
    public class SearchRepository : ISearchRepository
    {
        private readonly BookDbContext context;

        public SearchRepository(BookDbContext context)
        {
            this.context = context;
        }

        public async Task<Option?> GetOptionByCodeAsync(string optionCode)
        {
            return await context.Options.SingleOrDefaultAsync(o => o.OptionCode == optionCode);
        }

        public async Task<List<Option>> GetOptionsAsync(string destination)
        {
            return await context.Options
                .Where(o => o.ArrivalAirport == destination)
                .ToListAsync();
        }
    }
}
