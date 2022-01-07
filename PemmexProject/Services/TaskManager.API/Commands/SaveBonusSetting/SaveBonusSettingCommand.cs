using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.context;

namespace TaskManager.API.Commands.SaveBonusSetting
{
    public class SaveBonusSettingCommand : IRequest
    {
        public string businessIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public double limit_percentage { get; set; }
    }

    public class SaveBonusSettingCommandHandeler : IRequestHandler<SaveBonusSettingCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SaveBonusSettingCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(SaveBonusSettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var setting = await _context.BonusSettings
                     .FirstOrDefaultAsync(b => b.businessIdentifier == request.businessIdentifier);
                if(setting == null)
                {
                    var bonus_setting = _mapper.Map<Database.Entities.BonusSettings>(request);
                    _context.BonusSettings.Add(bonus_setting);
                }
                else
                {
                    setting.limit_percentage = request.limit_percentage;
                    _context.BonusSettings.Update(setting);
                }

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
