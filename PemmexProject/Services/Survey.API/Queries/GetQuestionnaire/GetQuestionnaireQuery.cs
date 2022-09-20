using AutoMapper;
using MediatR;
using Survey.API.Database.Interfaces;
using Survey.API.Dto;

namespace Survey.API.Queries.GetQuestionnaire
{
    public class GetQuestionnaireQuery : IRequest<List<SurveyQuestionsDto>>
    {
    }

    public class GetQuestionnaireQueryHandler : IRequestHandler<GetQuestionnaireQuery, List<SurveyQuestionsDto>>
    {
        private readonly IEmployeeSurvey _context;
        private readonly IMapper _mapper;

        public GetQuestionnaireQueryHandler(IEmployeeSurvey context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SurveyQuestionsDto>> Handle(GetQuestionnaireQuery request, CancellationToken cancellationToken)
        {
            var survey = await _context.GetQuestionnaire();
            
            return survey.ToList();
        }
    }
}
