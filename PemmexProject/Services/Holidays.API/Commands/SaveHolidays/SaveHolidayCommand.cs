using AutoMapper;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Enumerations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Commands.SaveHolidays
{
    public class SaveHolidayCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string costcenterIdentifier { get; set; }
        public Guid SubsituteId { get; set; }
        public string SubsituteIdentifier { get; set; }
        public DateTime? HolidayStartDate { get; set; }
        public DateTime? HolidayEndDate { get; set; }
        public HolidayStatus HolidayStatus { get; set; }
        public HolidayTypes holidayType { get; set; }
        public string Message { get; set; }
    }

    public class SaveHolidayCommandHandeler : IRequestHandler<SaveHolidayCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SaveHolidayCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(SaveHolidayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var holiday = _mapper.Map<EmployeeHolidays>(request);
                _context.EmployeeHolidays.Add(holiday);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
