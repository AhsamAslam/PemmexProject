using Dapper;
using Survey.API.Commands.CreateEmployeeSurvey.EmployeeSurveyRequests;
using Survey.API.Commands.SaveEmployeeSurvey;
using Survey.API.Database.Entities;
using Survey.API.Database.Interfaces;
using Survey.API.Dto;
using System.Data;
using System.Data.SqlClient;

namespace Survey.API.Database.Repositories
{
    public class EmployeeSurveyRepository: IEmployeeSurvey
    {
        #region
        private IDbConnection db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string CurrentUser;
        #endregion
        public EmployeeSurveyRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("SurveyConnection"));
            _httpContextAccessor = httpContextAccessor;
            CurrentUser = Convert.ToString(_httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "sub")?.Value);

        }
        public async Task<int> CreateEmployeeSurvey(List<GenerateEmployeeSurveyRequest> employeeSurveys)
        {
            try
            {
                var EmployeeSurveySql = @"INSERT INTO EmployeeSurvey(employeeIdentifier,isSurveySubmitted,organizationSurveyId,employeeName,managerIdentifier,managerName,businessIdentifier,Created,CreatedBy) 
                                            VALUES (@employeeIdentifier,0, @organizationSurveyId,@employeeName,@managerIdentifier,@managerName,@businessIdentifier, GETDATE(),'" + CurrentUser + "')";
                return await db.ExecuteAsync(EmployeeSurveySql, employeeSurveys).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Entities.EmployeeSurvey>> GetEmployeeSurvey(string employeeIdentifier, string organizationIdentifier)
        {
            try
            {
                var sql = @"  select e.employeeIdentifier,sq.segmentId,sq.segmentName,sq.questionId,sq.questionName,osq.surveyRate,
                               osq.surveyComments,e.employeeName,e.managerIdentifier,e.managerName,e.businessIdentifier, e.organizationSurveyId
                               from (select top 1 o.organizationSurveyId from OrganizationSurvey o 
                               where o.organizationIdentifier = @organizationIdentifier
                               order by o.organizationSurveyDate desc) a 
                               inner join OrganizationSurveyQuestion osq on osq.organizationSurveyId = a.organizationSurveyId
                               inner join SurveyQuestion sq on sq.surveyQuestionId = osq.surveyQuestionId
                               inner join EmployeeSurvey e on e.organizationSurveyId = a.organizationSurveyId
                               And e.employeeIdentifier = @employeeIdentifier";
                return await db.QueryAsync<Entities.EmployeeSurvey>(sql, new { @employeeIdentifier = employeeIdentifier, @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SurveySummaryDto>> GetOrganizationSurveyAverage(string organizationIdentifier)
        {
            try
            {
                var Sql = @"Select AVG(osq.surveyRate)* (qs.surveyQuestionEngagement / 100) avgSurveyRate, cast(qs.segmentId as nvarchar(50)) segmentId, qs.segmentName, cast(qs.questionId as nvarchar(50)) questionId, qs.questionName from OrganizationSurveyQuestion osq
                 inner join (Select top(1) os.organizationSurveyId from OrganizationSurvey os where os.organizationIdentifier = @organizationIdentifier order by os.organizationSurveyDate desc) o
                 on osq.organizationSurveyId = o.organizationSurveyId
                 inner join SurveyQuestion qs
                 on qs.surveyQuestionId = osq.surveyQuestionId
                 group by osq.surveyRate, qs.segmentId, qs.questionId, qs.segmentName, qs.questionName, qs.surveyQuestionEngagement";
                return await db.QueryAsync<SurveySummaryDto>(Sql, new { @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<IEnumerable<SurveyQuestionsDto>> GetQuestionnaire()
        {
            try
            {
                var Sql = @" Select * from SurveyQuestion";
                return await db.QueryAsync<SurveyQuestionsDto>(Sql).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SurveySummaryDto>> GetSurveyAverage(List<string> employeeIdentifier)
        {
            try
            {
                var Sql = @"Select AVG(osq.surveyRate)* (qs.surveyQuestionEngagement / 100) avgSurveyRate, cast(qs.segmentId as nvarchar(50)) segmentId, qs.segmentName, cast(qs.questionId as nvarchar(50)) questionId, qs.questionName from OrganizationSurveyQuestion osq
                 inner join 
				 (Select top(1) os.organizationSurveyId from OrganizationSurvey os
				 inner join EmployeeSurvey es on os.organizationSurveyId = es.organizationSurveyId
				 where es.employeeIdentifier in @employeeIdentifiers  order by os.organizationSurveyDate desc) o
                 on osq.organizationSurveyId = o.organizationSurveyId
                 inner join SurveyQuestion qs
                 on qs.surveyQuestionId = osq.surveyQuestionId
                 group by osq.surveyRate, qs.segmentId, qs.questionId, qs.segmentName, qs.questionName, qs.surveyQuestionEngagement";
                return await db.QueryAsync<SurveySummaryDto>(Sql, new { @employeeIdentifiers = employeeIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> SaveEmployeeSurvey(List<UpdateEmployeeSurveyRequest> updateEmployeeSurveyRequest)
        {
            try
            {
                var EmployeeSurveySql = @"Update osq
                                          set
                                          osq.surveyRate = @surveyRate,
                                          osq.surveyComments = @surveyComments
                                          from OrganizationSurveyQuestion osq
                                          inner join SurveyQuestion sq 
                                          on sq.questionId = @questionId 
                                          and sq.segmentId = @segmentId
                                          inner join EmployeeSurvey es
                                          on es.employeeIdentifier = @employeeIdentifier
                                          and es.organizationSurveyId = osq.organizationSurveyId";
                return await db.ExecuteAsync(EmployeeSurveySql, updateEmployeeSurveyRequest).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
