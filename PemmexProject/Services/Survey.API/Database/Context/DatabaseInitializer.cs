using Microsoft.EntityFrameworkCore;
using Survey.API.Database.Entities;


namespace Survey.API.Database.Context
{
    public static class DatabaseInitializer
    {
        public static void PopulateSurvey(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var _survey = serviceScope.ServiceProvider.GetRequiredService<SurveyContext>();
            var task = Task.Run(async () =>
            {
                var surveyQuestion = await _survey.SurveyQuestion.Where(r => r.segmentName == "NPS").FirstOrDefaultAsync();
                if (surveyQuestion == null)
                {
                    var NPS = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "NPS", questionId = System.Guid.NewGuid(), questionName = "I would recommend my employer to friends' and family as great place to work", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 100.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now};
                    await _survey.SurveyQuestion.AddAsync(NPS);
                    var NPS1 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "NPS", questionId = System.Guid.NewGuid(), questionName = "I would prefer my existing job over an offer from another company with same pay and role", surveyQuestionEngagement = 25.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(NPS1);
                    var NPS2 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "NPS", questionId = System.Guid.NewGuid(), questionName = "Overall, I am satisfied with my current job in the company", surveyQuestionEngagement = 25.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(NPS2);

                    var Strategy = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Strategy", questionId = System.Guid.NewGuid(), questionName = "I can relate my daily work to Company's targets and Strategy", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Strategy);
                    var Strategy1 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Strategy", questionId = System.Guid.NewGuid(), questionName = "I am motivated by the purpose or strategy of our company", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Strategy1);

                    var CareerGrowth = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Career Growth", questionId = System.Guid.NewGuid(), questionName = "I feel that I am growing professionally", surveyQuestionEngagement = 34.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(CareerGrowth);
                    var CareerGrowth1 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Career Growth", questionId = System.Guid.NewGuid(), questionName = "I can see career path in my current company", surveyQuestionEngagement = 33.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(CareerGrowth1);
                    var CareerGrowth2 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Career Growth", questionId = System.Guid.NewGuid(), questionName = "My job enables learning on most of the days", surveyQuestionEngagement = 33.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(CareerGrowth2);

                    var Health = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Health", questionId = System.Guid.NewGuid(), questionName = "The demands of my workload are reasaonable and manageable", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Health);
                    var Health1 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Health", questionId = System.Guid.NewGuid(), questionName = "My work envrionment contributes positively to perform my job", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Health1);

                    var Performance = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Performance", questionId = System.Guid.NewGuid(), questionName = "I know what is expected of me in my current role", surveyQuestionEngagement = 34.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Performance);
                    var Performance1 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Performance", questionId = System.Guid.NewGuid(), questionName = "My bonus targets have been clearly communicated to me", surveyQuestionEngagement = 33.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Performance1);
                    var Performance2 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Performance", questionId = System.Guid.NewGuid(), questionName = "I get feedback about my work regularly", surveyQuestionEngagement = 33.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Performance2);

                    var Leadership = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Leadership", questionId = System.Guid.NewGuid(), questionName = "My immediate Manager supports me when required", surveyQuestionEngagement = 25.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Leadership);
                    var Leadership1 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Leadership", questionId = System.Guid.NewGuid(), questionName = "My immediate Manager is open to ideas and reachable", surveyQuestionEngagement = 25.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Leadership1);
                    var Leadership2 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Leadership", questionId = System.Guid.NewGuid(), questionName = "My immediate manager cares about me as a person", surveyQuestionEngagement = 25.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Leadership2);
                    var Leadership3 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Leadership", questionId = System.Guid.NewGuid(), questionName = "I trust Senior Leadership of the company to lead this organization to success", surveyQuestionEngagement = 25.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Leadership3);

                    var Appreciation = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Appreciation", questionId = System.Guid.NewGuid(), questionName = "I feel appreciated as an employee", surveyQuestionEngagement = 100.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Appreciation);

                    var Impact = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Impact", questionId = System.Guid.NewGuid(), questionName = "I can see how the work I do has an impact on the success of the organization", surveyQuestionEngagement = 100.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Impact);

                    var Rewards = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Rewards", questionId = System.Guid.NewGuid(), questionName = "I am fairly compensated for my efforts towards company's success", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Rewards);
                    var Rewards1 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Rewards", questionId = System.Guid.NewGuid(), questionName = "My pay and benefits are in line with my efforts and current role", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Rewards1);

                    var Meaningful = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Meaningful", questionId = System.Guid.NewGuid(), questionName = "My work has significance to me personally and professionally", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Meaningful);
                    var Meaningful1 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Meaningful", questionId = System.Guid.NewGuid(), questionName = "I am doing meaningful work for the company and customers", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Meaningful1);

                    var Flexibility = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Flexibility", questionId = System.Guid.NewGuid(), questionName = "My job gives flexibility to meet the needs of my personal life", surveyQuestionEngagement = 100.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Flexibility);

                    var Support = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Support", questionId = System.Guid.NewGuid(), questionName = "I have access to materials and tools to do my job properly", surveyQuestionEngagement = 100.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Support);

                    var Autonomy = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Autonomy", questionId = System.Guid.NewGuid(), questionName = "I have enough flexibility to perform my job properly", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Autonomy);
                    var Autonomy1 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Autonomy", questionId = System.Guid.NewGuid(), questionName = "I am empowered to perform my work in a way that allows me to do what I do best", surveyQuestionEngagement = 50.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Autonomy1);

                    var Fairness = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Fairness", questionId = System.Guid.NewGuid(), questionName = "People of all backgrounds and ethnicity are treated fairly in our company", surveyQuestionEngagement = 100.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Fairness);

                    var SiblingsSatisfaction = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Siblings Satisfaction", questionId = System.Guid.NewGuid(), questionName = "My opinions are valued in my team", surveyQuestionEngagement = 34.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(SiblingsSatisfaction);
                    var SiblingsSatisfaction1 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Siblings Satisfaction", questionId = System.Guid.NewGuid(), questionName = "I am respected and feel safe to share my opinion in the team", surveyQuestionEngagement = 33.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(SiblingsSatisfaction1);
                    var SiblingsSatisfaction2 = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Siblings Satisfaction", questionId = System.Guid.NewGuid(), questionName = "I can count on my team mates for support when required", surveyQuestionEngagement = 33.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(SiblingsSatisfaction2);

                    var Aspiration = new SurveyQuestion { segmentId = System.Guid.NewGuid(), segmentName = "Aspiration", questionId = System.Guid.NewGuid(), questionName = "I see myself in 2 years being", surveyQuestionEngagement = 0.00, surveyQuestionNPS = 0.00, surveyQuestionAttrition = 0.00, CreatedBy = "test", Created = DateTime.Now };
                    await _survey.SurveyQuestion.AddAsync(Aspiration);

                    await _survey.SaveChangesAsync();
                }
            });
            task.GetAwaiter().GetResult();
        }
    }
}
