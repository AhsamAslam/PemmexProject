using AutoMapper;
using MediatR;
using PemmexCommonLibs.Domain.Enums;
using Survey.API.Commands.CreateEmployeeSurvey.EmployeeSurveyRequests;
using Survey.API.Database.Interfaces;

namespace Survey.API.Commands.SaveEmployeeSurvey
{
    public class SaveEmployeeSurveyCommand : IRequest<int>
    {
        
        public List<UpdateEmployeeSurveyRequest> saveEmployeeSurveyRequests { get; set; }
    }
    
    public class SaveEmployeeSurveyCommandHandeler : IRequestHandler<SaveEmployeeSurveyCommand, int>
    {
        private readonly IEmployeeSurvey _employeeSurvey;

        private readonly IMapper _mapper;
        public SaveEmployeeSurveyCommandHandeler(IEmployeeSurvey employeeSurvey, IMapper mapper)
        {
            _employeeSurvey = employeeSurvey;
            _mapper = mapper;
        }
        public async Task<int> Handle(SaveEmployeeSurveyCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var empSurvey = await _employeeSurvey.SaveEmployeeSurvey(request.saveEmployeeSurveyRequests);
                return empSurvey;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
