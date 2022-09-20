using AutoMapper;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Commands.SaveHolidaySetting
{
    public class SaveHolidaySettingCommand : IRequest<ResponseMessage>
    {
        public HolidaySettingsDto setting { get; set; }
    }

    public class SaveHolidaySettingCommandHandeler : IRequestHandler<SaveHolidaySettingCommand, ResponseMessage>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SaveHolidaySettingCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseMessage> Handle(SaveHolidaySettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var con_settings = _mapper.Map<HolidaySettings>(request.setting);
                string return_message = "";
                var settings = _context.HolidaySettings.AsNoTracking()
                .FirstOrDefault(h => h.BusinessIdentifier == request.setting.BusinessIdentifier);
                if (settings != null)
                {
                    settings.HolidayCalendarYear = con_settings.HolidayCalendarYear;
                    settings.MaximumLimitHolidayToNextYear = con_settings.MaximumLimitHolidayToNextYear;
                    _context.HolidaySettings.Update(con_settings);
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
            catch(Exception)
            {
                throw;
            }
        }
    }
}
