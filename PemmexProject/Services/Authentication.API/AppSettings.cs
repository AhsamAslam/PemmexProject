using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API
{
    public class AppSettings
    {
        public string QueueConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}
