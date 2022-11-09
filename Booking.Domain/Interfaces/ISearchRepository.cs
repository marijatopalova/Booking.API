using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<Option>> GetOptionsAsync(string destination);
        Task<Option> GetOptionByCodeAsync(string optionCode);
    }
}
