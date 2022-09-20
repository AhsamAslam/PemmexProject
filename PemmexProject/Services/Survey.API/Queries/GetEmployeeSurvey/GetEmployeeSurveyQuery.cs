using AutoMapper;
using MediatR;
using Survey.API.Database.Interfaces;
using Survey.API.Dto;

namespace Survey.API.Queries.GetEmployeeSurvey
{
    public class GetEmployeeSurveyQuery : IRequest<List<EmployeeSurveyDto>>
    {
        public string employeeIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
    }
    public class GetEmployeeSurveyQueryHandler : IRequestHandler<GetEmployeeSurveyQuery, List<EmployeeSurveyDto>>
    {
        private readonly IEmployeeSurvey _context;
        private readonly IMapper _mapper;

        public GetEmployeeSurveyQueryHandler(IEmployeeSurvey context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeSurveyDto>> Handle(GetEmployeeSurveyQuery request, CancellationToken cancellationToken)
        {
            var survey = await _context.GetEmployeeSurvey(request.employeeIdentifier, request.organizationIdentifier);
            return _mapper.Map<List<EmployeeSurveyDto>>(survey);
        }
    }
}
