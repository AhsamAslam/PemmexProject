using AutoMapper;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;
using System.Linq;

namespace EmployeeTargets.API.Commands.UpdateHardTargetsWeightage
{
    public class UpdateHardTargetsWeightageCommand:IRequest
    {
        public int Id { get; set; }
        public string employeeIdentifier { get; set; }
        public double Weightage { get; set; }
    }
    public class UpdateHardTargetsWeightageCommandHandeler : IRequestHandler<UpdateHardTargetsWeightageCommand>
    {
        private readonly IHardTargetsRepository _targets;
        private readonly IMapper _mapper;
        public UpdateHardTargetsWeightageCommandHandeler(IHardTargetsRepository targets, IMapper mapper)
        {
            _targets = targets;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateHardTargetsWeightageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ///*var target = _mapper.Map<List<HardTargets>>(*/request.HardTargets);
                var weightage = await _targets.ShowHardTargets(request.employeeIdentifier);
                if (weightage == null || weightage.Count() == 0)
                {
                    throw new Exception("Not Found");
                }
                else
                {
                    var updateWeightage = weightage.FirstOrDefault(x => x.HardTargetsId == request.Id);
                    var totalWeightage = weightage.Sum(x => x.Weightage);

                    var checkWeightage = (totalWeightage - updateWeightage.Weightage) + request.Weightage;
                    if (checkWeightage > 100.00)
                    {
                        throw new Exception("Weighatge exceed 100%");
                    }
                    else
                    {
                        await _targets.UpdateHardTargetsWeightage(request.Id, request.Weightage);
                    }
                }

                
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
