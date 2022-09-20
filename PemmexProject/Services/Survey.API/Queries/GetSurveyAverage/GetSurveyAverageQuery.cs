using AutoMapper;
using MediatR;
using Survey.API.Database.Interfaces;
using Survey.API.Dto;

namespace Survey.API.Queries.GetSurveyAverage
{
    public class GetSurveyAverageQuery : IRequest<List<SurveySummaryDto>>
    {
        public List<string> employeeIdentifier { get; set; }
    }
    public class GetSurveyAverageQueryHandler : IRequestHandler<GetSurveyAverageQuery, List<SurveySummaryDto>>
    {
        private readonly IEmployeeSurvey _context;
        private readonly IMapper _mapper;

        public GetSurveyAverageQueryHandler(IEmployeeSurvey context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SurveySummaryDto>> Handle(GetSurveyAverageQuery request, CancellationToken cancellationToken)
        {
            var surveyAvg = await _context.GetSurveyAverage(request.employeeIdentifier);
            return surveyAvg.ToList();
        }
    }
}
