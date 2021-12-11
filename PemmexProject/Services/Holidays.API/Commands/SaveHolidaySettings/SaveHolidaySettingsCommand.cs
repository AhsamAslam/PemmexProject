using AutoMapper;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Dtos;
using MediatR;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Commands.SaveHolidaySettings
{
    public class SaveHolidaySettingsCommand : IRequest<ResponseMessage>
    {
        public List<HolidaySettingsDto> settings { get; set; }
    }

    public class SaveHolidaySettingsCommandHandeler : IRequestHandler<SaveHolidaySettingsCommand, ResponseMessage>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SaveHolidaySettingsCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseMessage> Handle(SaveHolidaySettingsCommand request, CancellationToken cancellationToken)
        {
           if(request.settings.Count > 0)
           {
                var con_settings = _mapper.Map<List<HolidaySettings>>(request.settings);
                string return_message = "";
                var settings = _context.HolidaySettings.
                Where(h => h.OrganizationIdentifier == request.settings.FirstOrDefault().OrganizationIdentifier).ToList();
                if (settings.Count > 0)
                {
                    _context.HolidaySettings.RemoveRange(settings);
                    _context.HolidaySettings.AddRange(con_settings);
                    return_message = "Settings Updated";
                }
                else
                {
                    _context.HolidaySettings.AddRange(con_settings);
                    return_message = "Settings Saved";
                }

                await _context.SaveChangesAsync(cancellationToken);
                return new ResponseMessage(true, EResponse.OK, return_message, null);
            }
            else
            {
                return new ResponseMessage(true, EResponse.NoData,"No Data in the request", null);
            }
           
        }
    }
}
