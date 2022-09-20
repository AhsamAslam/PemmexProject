using Dapper;
using Survey.API.Database.Interfaces;
using Survey.API.Dto;
using System.Data;
using System.Data.SqlClient;

namespace Survey.API.Database.Repositories
{
    public class OrganizationSurveryRepository:IOrganizationSurvey
    {
        #region
        private IDbConnection db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string CurrentUser;
        #endregion
        public OrganizationSurveryRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("SurveyConnection"));
            _httpContextAccessor = httpContextAccessor;
            CurrentUser = Convert.ToString(_httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "sub")?.Value);

        }

        public async Task<int> AddOrganizationSurvey(OrganizationSurveyDto organizationSurveys)
        {
            try
            {
                var OrganizationSurveySql = @"INSERT INTO OrganizationSurvey (organizationIdentifier, organizationSurveyDate, Created, CreatedBy) 
                             OUTPUT INSERTED.organizationSurveyId
			                 VALUES (@organizationIdentifier, @organizationSurveyDate, GETDATE(),'" + CurrentUser + "')";
                var Id = db.QuerySingle<int>(OrganizationSurveySql, new { organizationIdentifier = organizationSurveys.organizationIdentifier, organizationSurveyDate = organizationSurveys.organizationSurveyDate });

                string[] segmentId = organizationSurveys.SurveyQuestion.Select(x => x.segmentId).ToArray();
                string[] questionId = organizationSurveys.SurveyQuestion.Select(x => x.questionId).ToArray();
                var SurveyQuestionSql = @"select surveyQuestionId from SurveyQuestion where segmentId in @segmentId And questionId in @questionId";
                
                var SurveyQuestionId = await db.QueryAsync<int>(SurveyQuestionSql, new { @segmentId = segmentId, @questionId = questionId }).ConfigureAwait(false);

                string query = "";
                foreach (var item in SurveyQuestionId)
                {
                    if (SurveyQuestionId.ToList().LastOrDefault() == item)
                    {
                        query += "(" + item + "," + Id + "," + "' '" + ")";
                    }
                    else
                    {
                        query += "(" + item + "," + Id + "," + "' '" + "),";
                    }
                    
                }
                var OrgSurveyQuestion = @"INSERT INTO OrganizationSurveyQuestion (surveyQuestionId,organizationSurveyId,surveyComments) VALUES" + query;
                await db.ExecuteAsync(OrgSurveyQuestion).ConfigureAwait(false);

                return Id;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
