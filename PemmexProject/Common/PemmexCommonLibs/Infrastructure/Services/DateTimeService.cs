using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PemmexCommonLibs.Application.Interfaces;

namespace PemmexCommonLibs.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
        public int DaysInMonth => DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
    }
}
