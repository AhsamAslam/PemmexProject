using Compensation.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Repositories.Interface
{
    public interface IJobCatalogue
    {
        Task AddJobCatalogue(JobCatalogue JobCatalogue);
        Task<JobCatalogue> GetJobCatalogueByOrganizationIdentifierAndJobFunctionAndGrade(string OrganizationIdentifier,
            string JobFunction, string Grade);
    }
}
