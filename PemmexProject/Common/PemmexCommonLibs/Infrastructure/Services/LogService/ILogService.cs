using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Application.Interfaces
{
    public interface ILogService
    {
        public Task WriteLogAsync(Exception ex, string fileName);
    }
}
