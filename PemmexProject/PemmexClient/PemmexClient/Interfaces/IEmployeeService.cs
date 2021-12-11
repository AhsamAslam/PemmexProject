using PemmexClient.Models;
using PemmexCommonLibs.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexClient.Interfaces
{
    public interface IEmployeeService
    {
        Task<ResponseMessage> GetAllEmployee(int organizationId);
        Task<ResponseMessage> GetEmployee(int id);
        Task<ResponseMessage> CreateEmployee(Employee employee);
        Task<ResponseMessage> UpdateEmployee(Employee employee);
        Task<ResponseMessage> GetManagers(int organizationId);
    }
}
