using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common;
using Survey.API.Database.Entities;

namespace Survey.API.Database.Context
{
    public class SurveyContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SurveyContext(DbContextOptions options,
           IDateTime dateTime, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrganizationSurveyQuestion>().HasKey(os => new { os.surveyQuestionId , os.organizationSurveyId });

            builder.Entity<OrganizationSurveyQuestion>()
                .HasOne<SurveyQuestion>(sc => sc.SurveyQuestion)
                .WithMany(s => s.OrganizationSurveyQuestion)
                .HasForeignKey(sc => sc.surveyQuestionId);


            builder.Entity<OrganizationSurveyQuestion>()
                .HasOne<OrganizationSurvey>(sc => sc.OrganizationSurvey)
                .WithMany(s => s.OrganizationSurveyQuestion)
                .HasForeignKey(sc => sc.organizationSurveyId);

            builder.Entity<EmployeeSurvey>()
            .Ignore(i => i.questionId)
            .Ignore(i => i.questionName)
            .Ignore(i => i.segmentId)
            .Ignore(i => i.segmentName);


        }
        public DbSet<SurveyQuestion> SurveyQuestion { get; set; }
        public DbSet<OrganizationSurvey> OrganizationSurvey { get; set; }
        public DbSet<EmployeeSurvey> EmployeeSurvey { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
                {
                    if (entry.State != EntityState.Unchanged) //ignore unchanged entities and history tables
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                entry.Entity.CreatedBy = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "sub")?.Value;
                                entry.Entity.Created = _dateTime.Now;
                                break;

                            case EntityState.Modified:
                                entry.Entity.LastModifiedBy = _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "sub")?.Value;
                                entry.Entity.LastModified = _dateTime.Now;
                                break;
                        }

                    }
                }
                var result = await base.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
