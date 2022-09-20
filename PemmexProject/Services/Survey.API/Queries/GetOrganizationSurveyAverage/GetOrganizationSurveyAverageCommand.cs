using AutoMapper;
using MediatR;
using Survey.API.Database.Interfaces;
using Survey.API.Dto;

namespace Survey.API.Queries.GetOrganizationSurveyAverage
{
    public class GetOrganizationSurveyAverageCommand : IRequest<List<SurveySummaryDto>>
    {
        public string organizationIdentifier { get; set; }
    }
    public class GetOrganizationSurveyAverageCommandHandler : IRequestHandler<GetOrganizationSurveyAverageCommand, List<SurveySummaryDto>>
    {
        private readonly IEmployeeSurvey _context;
        private readonly IMapper _mapper;

        public GetOrganizationSurveyAverageCommandHandler(IEmployeeSurvey context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SurveySummaryDto>> Handle(GetOrganizationSurveyAverageCommand request, CancellationToken cancellationToken)
        {
            var surveyAvg = await _context.GetOrganizationSurveyAverage(request.organizationIdentifier);
            return surveyAvg.ToList();
        }
    }
}
