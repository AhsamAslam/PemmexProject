using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.context;

namespace TaskManager.API.Commands.DeleteBonusSetting
{
    public class DeleteBonusSettingCommand : IRequest
    {
        public string businessIdentifier { get; set; }
    }

    public class DeleteBonusSettingCommandHandeler : IRequestHandler<DeleteBonusSettingCommand>
    {
        private readonly IApplicationDbContext _context;
        public DeleteBonusSettingCommandHandeler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteBonusSettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var setting = await _context.BonusSettings
                     .FirstOrDefaultAsync(b => b.businessIdentifier == request.businessIdentifier);
                if (setting != null)
                {
                    _context.BonusSettings.Remove(setting);
                    await _context.SaveChangesAsync(cancellationToken);
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
