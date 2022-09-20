using AutoMapper;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Commands.SaveHolidaySettings
{
    public class UploadHolidaySettingsCommand : IRequest<int>
    {
        public HolidaySettings settings { get; set; }
    }

    public class UploadHolidaySettingsCommandHandeler : IRequestHandler<UploadHolidaySettingsCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UploadHolidaySettingsCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(UploadHolidaySettingsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _context.HolidaySettings.Add(request.settings);
                await _context.SaveChangesAsync(cancellationToken);
                return request.settings.HolidaySettingsId;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
