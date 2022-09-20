using AutoMapper;
using MediatR;
using Survey.API.Database.Interfaces;
using Survey.API.Dto;

namespace Survey.API.Commands.CreateOrganizationSurvey
{
    public class CreateOrganizationSurveyCommand : IRequest<int>
    {
        public OrganizationSurveyDto organizationSurvey { get; set; }
    }

    public class CreateOrganizationSurveyCommandHandeler : IRequestHandler<CreateOrganizationSurveyCommand, int>
    {
        private readonly IOrganizationSurvey _organizationSurvey;
       
        private readonly IMapper _mapper;
        public CreateOrganizationSurveyCommandHandeler(IOrganizationSurvey organizationSurvey, IMapper mapper)
        {
            _organizationSurvey = organizationSurvey;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateOrganizationSurveyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var target = _mapper.Map<List<HardTargets>>(request.HardTargets);
                //await _targets.CreateHardTargets(target);
                var oSurvey = await _organizationSurvey.AddOrganizationSurvey(request.organizationSurvey);
                return oSurvey;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
