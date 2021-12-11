using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Enumerations
{
    public enum TaskStatuses
    {
        None,
        Initiated,
        Approved,
        Pending,
        Declined,
        Cancelled

    }
}
