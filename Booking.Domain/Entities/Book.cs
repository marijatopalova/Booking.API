using Booking.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class Book
    {
        [Key]
        public string BookingCode { get; set; }
        public int SleepTime { get; set; }
        public DateTime BookingTime { get; set; }
        public SearchTypeEnum SearchType { get; set; }
    }
}
