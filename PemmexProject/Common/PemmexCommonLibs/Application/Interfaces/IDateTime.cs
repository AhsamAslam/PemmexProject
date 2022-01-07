using System;

namespace PemmexCommonLibs.Application.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
        int DaysInMonth { get; }
    }
}
