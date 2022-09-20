using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Organization.API.Database.Entities;
using Organization.API.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Database.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region
        private IDbConnection db;
        #endregion
        public EmployeeRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("OrganizationConnection"));
        }

        public async Task<IEnumerable<Employee>> GetBusinessUnitsHeads(string organizationIdentifier)
        {
            try
            {
                var sql = @" select e.* from Employees e where e.ManagerIdentifier =
                                (select top 1 EmployeeIdentifier from Employees
                                inner join Businesses b on ParentBusinessId = @organizationIdentifier
                                where Title ='CEO')";
                return await db.QueryAsync<Employee>(sql, new { @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees(string[] employeeIdentifiers)
        {
            try
            {
                var sql = @"select e.*,(m.FirstName + m.LastName) ManagerName,c.CostCenterIdentifier,c.CostCenterName,
                            b.BusinessIdentifier,b.BusinessName,b.OrganizationCountry from Employees e 
					        inner join CostCenters c on e.CostCenterId = c.CostCenterId
                            inner join Businesses b on b.Id = e.BusinessId
                            inner join Employees m on m.EmployeeIdentifier = e.ManagerIdentifier
					        and e.EmployeeIdentifier in @employeeIdentifiers";
                return await db.QueryAsync<Employee>(sql, new { @employeeIdentifiers = employeeIdentifiers }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetSiblings(string employeeIdentifier)
        {
            try
            {
                var sql = @"Select e.*,(m.FirstName + m.LastName) ManagerName,c.CostCenterIdentifier,c.CostCenterName,
                            b.BusinessIdentifier,b.BusinessName,b.OrganizationCountry from Employees e 
                            inner join CostCenters c on e.CostCenterId = c.CostCenterId
                            inner join Businesses b on b.Id = e.BusinessId
                            inner join Employees m on m.EmployeeIdentifier = e.ManagerIdentifier
                            and m.EmployeeIdentifier = @employeeIdentifier";
                return await db.QueryAsync<Employee>(sql,
                     new { @employeeIdentifier = employeeIdentifier })
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetTeamMembers(string employeeIdentifier)
        {
            try
            {
                var sql = @"Select e.*,(m.FirstName + m.LastName) ManagerName,c.CostCenterIdentifier,c.CostCenterName,
                            b.BusinessIdentifier,b.BusinessName,b.OrganizationCountry from Employees e 
                            inner join CostCenters c on e.CostCenterId = c.CostCenterId
                            inner join Businesses b on b.Id = e.BusinessId
                            inner join Employees m on m.EmployeeIdentifier = e.ManagerIdentifier
                            and e.ManagerIdentifier = @employeeIdentifier";
                return await db.QueryAsync<Employee>(sql,
                     new { @employeeIdentifier = employeeIdentifier })
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetTeamMembersHierarchy(string employeeIdentifier)
        {
            try
            {
                var sql = @" with emp as(
                            select 
	                              e.EmployeeId
                                  ,e.BusinessId,e.CostCenterId,e.Emp_Guid,e.FirstName,e.LastName,e.MiddleName,e.Title,e.EmployeeDob,e.EmployeeIdentifier,e.Gender,e.ManagerIdentifier,b.ParentBusinessId OrganizationIdentifier,e.Grade
                                  ,e.StreetAddress,e.HouseNumber,e.Muncipality,e.PostalCode,e.Province,e.country,e.CountryCellNumber,e.PhoneNumber,e.Email,e.Nationality,e.FirstLanguage,e.FirstLanguageSkills,e.SecondLanguage,e.SecondLanguageSkills,e.ThirdLanguage,
	                              e.ThirdLanguageSkills,e.JobFunction,
	                              (m.FirstName + m.LastName) ManagerName,c.CostCenterIdentifier,c.CostCenterName,b.BusinessIdentifier,b.BusinessName,b.OrganizationCountry from Employees e
                            inner join CostCenters c on e.CostCenterId = c.CostCenterId
                            inner join Businesses b on b.Id = e.BusinessId
                            inner join Employees m on m.EmployeeIdentifier = e.ManagerIdentifier
                            and e.ManagerIdentifier = @employeeIdentifier
                            union all
                            select 
		                            p.EmployeeId
                                  ,p.BusinessId,p.CostCenterId,p.Emp_Guid,p.FirstName,p.LastName,p.MiddleName,p.Title,p.EmployeeDob,p.EmployeeIdentifier,p.Gender,p.ManagerIdentifier,b.ParentBusinessId OrganizationIdentifier,p.Grade
                                  ,p.StreetAddress,p.HouseNumber,p.Muncipality,p.PostalCode,p.Province,p.country,p.CountryCellNumber,p.PhoneNumber,p.Email,p.Nationality,p.FirstLanguage,p.FirstLanguageSkills,p.SecondLanguage,p.SecondLanguageSkills,p.ThirdLanguage
                             ,p.ThirdLanguageSkills,p.JobFunction
                            ,(m.FirstName + m.LastName) ManagerName,c.CostCenterIdentifier,c.CostCenterName,b.BusinessIdentifier,b.BusinessName,b.OrganizationCountry from Employees p
                            inner join Businesses b on b.Id = p.BusinessId
                            inner join CostCenters c on p.CostCenterId = c.CostCenterId
                            inner join Employees m on m.EmployeeIdentifier = p.ManagerIdentifier and p.ManagerIdentifier = 'FI2345862'
                            inner join emp e on e.EmployeeIdentifier = p.ManagerIdentifier
                            )

                            select * from emp;";
                return await db.QueryAsync<Employee>(sql
                    //, (a, b) => {
                    //    a.Businesses = b;
                    //    return a;
                    //},
                     ,new { @employeeIdentifier = employeeIdentifier })
                     //splitOn: "BusinessId")
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
