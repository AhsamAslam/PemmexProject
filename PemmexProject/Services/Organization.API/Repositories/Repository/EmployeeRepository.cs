using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Organization.API.Entities;
using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Repository
{
    public class EmployeeRepository : IEmployee
    {
        #region
        private IDbConnection db;
        #endregion
        public EmployeeRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("OrganizationConnection"));
        }
        public async Task<Employee> AddEmployee(Employee Employee)
        {
            try
            {
                var Sql = "INSERT INTO Employees(BusinessId, CostCenterId, Created, " +
                    "CreatedBy, Emp_Guid, FirstName, LastName, MiddleName, " +
                    "Title, EmployeeDob, EmployeeIdentifier, Gender, ManagerIdentifier," +
                    " OrganizationIdentifier, Grade, StreetAddress, HouseNumber, Muncipality, " +
                    "PostalCode, Province, country, CountryCellNumber, PhoneNumber, Email, " +
                    "Nationality, FirstLanguage, FirstLanguageSkills, SecondLanguage, " +
                    "SecondLanguageSkills, ThirdLanguage, ThirdLanguageSkills, JobFunction, " +
                    "Shift, IsActive) VALUES(@BusinessId, @CostCenterId, GetDate(), 'test'," +
                    " @Emp_Guid, @FirstName, @LastName, @MiddleName, @Title, @EmployeeDob, " +
                    "@EmployeeIdentifier, @Gender, @ManagerIdentifier, @OrganizationIdentifier, " +
                    "@Grade, @StreetAddress, @HouseNumber, @Muncipality, @PostalCode, @Province, " +
                    "@country, @CountryCellNumber, @PhoneNumber, @Email, @Nationality, " +
                    "@FirstLanguage, @FirstLanguageSkills, @SecondLanguage, @SecondLanguageSkills, " +
                    "@ThirdLanguage, @ThirdLanguageSkills, @JobFunction, @Shift, @IsActive)";
                await db.ExecuteAsync(Sql, Employee).ConfigureAwait(false);
                return Employee;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<int> DeleteEmployee(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByCostCenterIdentifier(string CostCenterIdentifier)
        {
            try
            {
                var Sql = "Select * from Employees e " +
                    "inner join CostCenters c on e.CostCenterId = c.CostCenterId " +
                    "inner join Businesses b on e.BusinessId = b.Id " +
                    "where c.CostCenterIdentifier = @CostCenterIdentifier and isnull(e.IsActive,1) = 1";
                return await db.QueryAsync<Employee>(Sql, new { @CostCenterIdentifier = CostCenterIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Employee> GetEmployeeByEmpGuid(Guid EmpGuid)
        {
            try
            {
                var Sql = "Select * from Employees e " +
                    "inner join CostCenters c on e.CostCenterId = c.CostCenterId " +
                    "inner join EmployeeContacts ec on e.EmployeeId = ec.EmployeeId " +
                    "where e.Emp_Guid = @EmpGuid and isnull(e.IsActive,1) = 1";
                return await db.QueryFirstAsync<Employee>(Sql, new { @EmpGuid = EmpGuid }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Employee> GetEmployeeByGuidId(Guid EmpGuid)
        {
            try
            {
                var Sql = "Select * from Employees where Emp_Guid = @EmpGuid";
                return await db.QueryFirstAsync<Employee>(Sql, new { @EmpGuid = EmpGuid }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByEmployeeIdentifier(string[] EmployeeIdentifier)
        {
            try
            {
                List<Employee> employee = new List<Employee>();
                foreach (var item in EmployeeIdentifier)
                {
                    var Sql = "Select * from Employees e " +
                    "inner join CostCenters c on e.CostCenterId = c.CostCenterId " +
                    "inner join EmployeeContacts ec on e.EmployeeId = ec.EmployeeId " +
                    "where e.EmployeeIdentifier = @EmployeeIdentifier and isnull(e.IsActive,1) = 1";
                    employee.Add(await db.QueryFirstAsync<Employee>(Sql, new { @EmployeeIdentifier = item }).ConfigureAwait(false));
                }

                return employee;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Employee> GetEmployeeByEmployeeIdentifier(string EmployeeIdentifier)
        {
            try
            {
                var Sql = "Select * from Employees e " +
                   "inner join CostCenters c on e.CostCenterId = c.CostCenterId " +
                   "inner join EmployeeContacts ec on e.EmployeeId = ec.EmployeeId " +
                   "where e.EmployeeIdentifier = @EmployeeIdentifier and isnull(e.IsActive,1) = 1";
                return await db.QueryFirstAsync<Employee>(Sql, new { @EmployeeIdentifier = EmployeeIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<Employee>> GetEmployeeById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeTreeForManager(string EmployeeIdentifier)
        {
            try
            {
                return await db.QueryAsync<Employee>("sp_GetEmployeeTreeForManager",
                      this.SetParameter(EmployeeIdentifier),
                      commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Employee> UpdateEmployee(Employee Employee)
        {
            try
            {
                var Sql = "UPDATE Employees SET BusinessId = @BusinessId," +
                    "CostCenterId = @CostCenterId,LastModified = @GETDATE()," +
                    "LastModifiedBy = @'test1',Emp_Guid = @Emp_Guid,FirstName = @FirstName," +
                    "LastName = @LastName,MiddleName = @MiddleName,Title = @Title," +
                    "EmployeeDob = @EmployeeDob,EmployeeIdentifier = @EmployeeIdentifier," +
                    "Gender = @Gender,ManagerIdentifier = @ManagerIdentifier," +
                    "OrganizationIdentifier = @OrganizationIdentifier,Grade = @Grade," +
                    "StreetAddress = @StreetAddress,HouseNumber = @HouseNumber," +
                    "Muncipality = @Muncipality,PostalCode = @PostalCode,Province = @Province," +
                    "country = @country,CountryCellNumber = @CountryCellNumber," +
                    "PhoneNumber = @PhoneNumber,Email = @Email,Nationality = @Nationality," +
                    "FirstLanguage = @FirstLanguage,FirstLanguageSkills = @FirstLanguageSkills," +
                    "SecondLanguage = @SecondLanguage,SecondLanguageSkills = @SecondLanguageSkills," +
                    "ThirdLanguage = @ThirdLanguage,ThirdLanguageSkills = @ThirdLanguageSkills," +
                    "JobFunction = @JobFunction,Shift = @Shift,IsActive = @IsActive " +
                    "WHERE EmployeeId = @EmployeeId";
                await db.ExecuteAsync(Sql, Employee).ConfigureAwait(false);
                return Employee;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private DynamicParameters SetParameter(string EmployeeIdentifier)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeIdentifier", EmployeeIdentifier);
            return param;
        }
    }
}
