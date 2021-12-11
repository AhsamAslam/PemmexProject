using PemmexClient.Models;
using PemmexCommonLibs.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexClient.Interfaces
{
    public interface IOrganizationService
    {
        Task<ResponseMessage> GetOrganization(int id);
    }
}
