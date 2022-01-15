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

namespace TaskManager.API.Commands.DeleteBonusSetting
{
    public class DeleteBonusSettingCommand : IRequest
    {
        public string businessIdentifier { get; set; }
    }

    public class DeleteBonusSettingCommandHandeler : IRequestHandler<DeleteBonusSettingCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IBonusSettings _bonusSettings;
        public DeleteBonusSettingCommandHandeler(IApplicationDbContext context, IBonusSettings bonusSettings)
        {
            _context = context;
            _bonusSettings = bonusSettings;
        }
        public async Task<Unit> Handle(DeleteBonusSettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var setting = await _context.BonusSettings
                //     .FirstOrDefaultAsync(b => b.businessIdentifier == request.businessIdentifier);
                var setting = await _bonusSettings.GetBonusSettingsByBusinessIdentifier(request.businessIdentifier);
                if (setting != null)
                {
                    //_context.BonusSettings.Remove(setting);
                    //await _context.SaveChangesAsync(cancellationToken);
                    await _bonusSettings.DeleteBonusSettings(setting);
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
