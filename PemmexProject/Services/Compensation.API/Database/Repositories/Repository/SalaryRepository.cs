using Compensation.API.Database.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Repositories.Repository
{
    public class SalaryRepository: ISalary
    {
        #region
        private IDbConnection db;
        #endregion
        public SalaryRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("CompensationConnection"));
        }
    }
}
