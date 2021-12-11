using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Enums
{
    public enum EResponse
    {
        OK = 200,
        UnexpectedError = 500,
        NoData = 401,
        ValidationError = 400,
        NoPermission = 403,
        UnSuccess = 400
    }
}
