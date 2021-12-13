using Organization.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Interface
{
    public interface IEmployeeContact
    {
        Task<IEnumerable<EmployeeContacts>> GetEmployeeContacts();
        Task<IEnumerable<EmployeeContacts>> GetEmployeeContactsById(int Id);
        Task<EmployeeContacts> AddEmployeeContacts(EmployeeContacts EmployeeContacts);
        Task<EmployeeContacts> UpdateEmployeeContacts(EmployeeContacts EmployeeContacts);
        Task<int> DeleteEmployeeContacts(int Id);
    }
}
