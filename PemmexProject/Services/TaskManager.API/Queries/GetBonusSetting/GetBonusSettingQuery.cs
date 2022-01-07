using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.context;
using TaskManager.API.Dtos;

namespace TaskManager.API.Queries.GetBonusSetting
{
    public class GetBonusSettingQuery : IRequest<BonusSettingsDto>
    {
        public string businessIdentifier { get; set; }
    }

    public class GetBonusSettingQueryHandeler : IRequestHandler<GetBonusSettingQuery, BonusSettingsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBonusSettingQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BonusSettingsDto> Handle(GetBonusSettingQuery request, CancellationToken cancellationToken)
        {
            var salary = await _context.BonusSettings
                .Where(e => e.businessIdentifier == request.businessIdentifier)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Database.Entities.BonusSettings, BonusSettingsDto>(salary);
        }
    }
}
