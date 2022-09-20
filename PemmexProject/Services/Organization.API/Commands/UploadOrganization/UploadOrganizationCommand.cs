using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Services;
using MediatR;
using Organization.API.Database.Context;
using Organization.API.Database.Entities;
using Organization.API.Dtos;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using PemmexCommonLibs.Infrastructure.Services;

namespace Organization.API.Commands.UploadOrganization
{
    public class UploadOrganizationCommand : IRequest
    {
        public List<BusinessRequestDto> businesses { get; set; }
        public List<EmployeeUploadRequest> employeeUploadRequests { get; set; }
        public List<CostCenterUploadRequest> costCenterUploads { get; set; }
        public List<CompensationUploadRequest> compensationUploadRequests { get; set; }
    }

    public class UploadOrganizationCommandHandeler : IRequestHandler<UploadOrganizationCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITopicPublisher _messagePublisher;
            private static int count  =0;
        public UploadOrganizationCommandHandeler(IApplicationDbContext context,IMapper mapper, ITopicPublisher messagePublisher)
        {
            _context = context;
            _mapper = mapper;
            _messagePublisher = messagePublisher;
        }
        public async Task<Unit> Handle(UploadOrganizationCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var users = new List<UserEntity>();
                var holidaysettingEntities = new List<HolidaySettingsEntity>();
                var holidays = new List<CompanyToEmployeeHolidayEntity>();
                var Businesses = _mapper.Map<List<BusinessRequestDto>, List<Business>>(request.businesses);
                var compensations = _mapper.Map<List<CompensationUploadRequest>, List<CompensationEntity>>(request.compensationUploadRequests);
                var costcenters = _mapper.Map<List<CostCenterUploadRequest>, List<CostCenter>>(request.costCenterUploads);
                _context.CostCenters.AddRange(costcenters);
                await _context.SaveChangesAsync(cancellationToken);
                foreach (var org in Businesses)
                {
                    org.IsActive = true;
                    var holidaySettingEntity = new HolidaySettingsEntity();
                    if (org.BusinessIdentifier != org.ParentBusinessId)
                    {
                        holidaySettingEntity.BusinessIdentifier = org.BusinessIdentifier;
                        holidaySettingEntity.HolidayCalendarYear = DateTime.Now.Month < 4 ? new DateTime(DateTime.Now.Year - 1, 4, 1) : new DateTime(DateTime.Now.Year, 4, 1);
                        holidaySettingEntity.MaximumLimitHolidayToNextYear = 15;
                        holidaySettingEntity.OrganizationIdentifier = org.ParentBusinessId;
                        holidaySettingEntity.HolidaySettingsIdentitfier = Guid.NewGuid();
                        holidaysettingEntities.Add(holidaySettingEntity);
                    }
                    if (request.employeeUploadRequests.Count > 0)
                    {
                        var employees = request.employeeUploadRequests.Where(x => x.OrganizationIdentifier == org.BusinessIdentifier).ToList();
                        foreach (var e in employees)
                        {
                            e.CostCenterId = costcenters.FirstOrDefault(c => c.CostCenterIdentifier == e.CostCenterIdentifier).CostCenterId;
                            e.IsActive = true;
                            //e.Role = string.IsNullOrEmpty(e.Role) ? Enum.GetName(Roles.User) : e.Role;
                            var u = _mapper.Map<EmployeeUploadRequest, UserEntity>(e);
                            List<string> r = new List<string>();
                            r.Add((!Enum.IsDefined(typeof(Roles), e.Role.ToLower()) || string.IsNullOrEmpty(e.Role)) ? Enum.GetName(Roles.user) : e.Role.ToLower());
                            u.Role = r.ToArray();
                            u.OrganizationIdentifier = org.ParentBusinessId;
                            u.BusinessIdentifier = org.BusinessIdentifier;
                            u.OrganizationCountry = org.OrganizationCountry;
                            u.IsPasswordReset = false;
                            var compensation = compensations.FirstOrDefault(c => c.EmployeeIdentifier == e.EmployeeIdentifier);
                            if(compensation != null)
                            {
                                compensation.organizationIdentifier = org.ParentBusinessId;
                                compensation.businessIdentifier = org.BusinessIdentifier;
                                compensation.currencyCode = org.CurrencyCode;
                            }
                            users.Add(u);
                            var holiday = _mapper.Map<HolidayUploadRequest, CompanyToEmployeeHolidayEntity>(e.holidayUploadRequest);
                            holiday.costcenterIdentifier = e.CostCenterIdentifier;
                            holiday.businessIdentifier = org.BusinessIdentifier;
                            holiday.organizationIdentifier = org.BusinessIdentifier;
                            holiday.HolidaySettingsIdentitfier = holidaySettingEntity.HolidaySettingsIdentitfier;
                            holidays.Add(holiday);
                            count++;

                        }
                        org.Employees = _mapper.Map<List<EmployeeUploadRequest>, List<Employee>>(employees);
                    }
                    //var o = _mapper.Map<BusinessDetailRequest, Business>(bD);
                    //parentBusiness.Businesses.Add(o);

                }
                Parallel.ForEach(users, async user =>
                {
                    await _messagePublisher.Publish<UserEntity>(user);

                });
                Parallel.ForEach(holidays, async holiday =>
                {
                    await _messagePublisher.Publish<CompanyToEmployeeHolidayEntity>(holiday);

                });
                Parallel.ForEach(holidaysettingEntities, async holiday =>
                {
                    await _messagePublisher.Publish<HolidaySettingsEntity>(holiday);

                });
                Parallel.ForEach(compensations, async c =>
                {
                    await _messagePublisher.Publish<CompensationEntity>(c);

                });
                _context.Businesses.AddRange(Businesses);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch(Exception e)
            {
                throw new Exception(count.ToString());
            }
        }
    }
}
