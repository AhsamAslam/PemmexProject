using AutoMapper;
using MediatR;
using PemmexCommonLibs.Domain.Enums;
using Survey.API.Commands.CreateEmployeeSurvey.EmployeeSurveyRequests;
using Survey.API.Database.Interfaces;
using Survey.API.Dto;

namespace Survey.API.Commands.CreateEmployeeSurvey
{
    public class CreateEmployeeSurveyCommand : IRequest<int>
    {
        public List<GenerateEmployeeSurveyRequest> generateEmployeeSurveyRequest { get; set; }

    }
    public class CreateEmployeeSurveyCommandHandeler : IRequestHandler<CreateEmployeeSurveyCommand, int>
    {
        private readonly IEmployeeSurvey _employeeSurvey;

        private readonly IMapper _mapper;
        public CreateEmployeeSurveyCommandHandeler(IEmployeeSurvey employeeSurvey, IMapper mapper)
        {
            _employeeSurvey = employeeSurvey;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateEmployeeSurveyCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var empSurvey = await _employeeSurvey.CreateEmployeeSurvey(request.generateEmployeeSurveyRequest);
                return empSurvey;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
