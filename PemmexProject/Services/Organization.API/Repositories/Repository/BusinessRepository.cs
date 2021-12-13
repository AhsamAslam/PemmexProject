using Organization.API.Entities;
using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Repository
{
    public class BusinessRepository : IBusiness
    {
        public Task<Business> AddBusiness(Business Business)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteBusiness(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Business>> GetBusiness()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Business>> GetBusinessById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Business> UpdateBusiness(Business Business)
        {
            throw new NotImplementedException();
        }
    }
}
