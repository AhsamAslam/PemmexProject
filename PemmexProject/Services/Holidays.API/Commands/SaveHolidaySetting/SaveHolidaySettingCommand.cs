using AutoMapper;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Dtos;
using Holidays.API.Repositories.Interface;
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
        private readonly IHolidaySettings _holidaySetting;
        private readonly IMapper _mapper;
        public SaveHolidaySettingCommandHandeler(IApplicationDbContext context, IHolidaySettings holidaySetting, IMapper mapper)
        {
            _context = context;
            _holidaySetting = holidaySetting;
            _mapper = mapper;
        }
        public async Task<ResponseMessage> Handle(SaveHolidaySettingCommand request, CancellationToken cancellationToken)
        {
            var con_settings = _mapper.Map<HolidaySettings>(request.setting);
            string return_message = "";
            var settings = _context.HolidaySettings.AsNoTracking()
            .FirstOrDefault(h => h.BusinessIdentifier == request.setting.BusinessIdentifier);
            if (settings != null)
            {
                settings.HolidayCalendarYear = con_settings.HolidayCalendarYear;
                settings.MaximumLimitHolidayToNextYear = con_settings.MaximumLimitHolidayToNextYear;
                await _holidaySetting.UpdateHolidaySetting(con_settings);
                return_message = "Settings Updated";
            }
            else
            {
                await _holidaySetting.AddHolidaySetting(con_settings);
                return_message = "Settings Saved";
            }

            //await _context.SaveChangesAsync(cancellationToken);
            return new ResponseMessage(true, EResponse.OK, return_message, null);

        }
    }
}
