using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.context;
using TaskManager.API.Database.Repositories.Interface;

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
        private readonly IBonusSettings _bonusSettings;
        private readonly IMapper _mapper;
        public SaveBonusSettingCommandHandeler(IBonusSettings bonusSettings, IMapper mapper)
        {
            _bonusSettings = bonusSettings;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(SaveBonusSettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var setting = await _context.BonusSettings
                //     .FirstOrDefaultAsync(b => b.businessIdentifier == request.businessIdentifier);
                var setting = await _bonusSettings.GetBonusSettingsByBusinessIdentifier(request.businessIdentifier);

                if (setting == null)
                {
                    var bonus_setting = _mapper.Map<Database.Entities.BonusSettings>(request);
                    //_context.BonusSettings.Add(bonus_setting);
                    await _bonusSettings.AddBonusSettings(bonus_setting);
                }
                else
                {
                    setting.limit_percentage = request.limit_percentage;
                    //_context.BonusSettings.Update(setting);
                    await _bonusSettings.UpdateBonusSettings(setting);
                }

                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
