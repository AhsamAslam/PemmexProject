using Organization.API.Entities;
using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Repository
{
    public class EmployeeContactsRepository : IEmployeeContact
    {
        public Task<EmployeeContacts> AddEmployeeContacts(EmployeeContacts EmployeeContacts)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteEmployeeContacts(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeContacts>> GetEmployeeContacts()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeContacts>> GetEmployeeContactsById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeContacts> UpdateEmployeeContacts(EmployeeContacts EmployeeContacts)
        {
            throw new NotImplementedException();
        }
    }
}
