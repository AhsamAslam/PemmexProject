using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.context;
using TaskManager.API.Database.Entities;
using TaskManager.API.Database.Repositories.Interface;
using TaskManager.API.Dtos;
using TaskManager.API.Enumerations;

namespace TaskManager.API.Commands.SaveSetting
{
    public class SaveSettings : IRequest
    {
        public ApprovalSettingDto setting { get; set; }
    }

    public class SaveSettingsCommandHandeler : IRequestHandler<SaveSettings>
    {
        private readonly IApprovalSettings _approvalSettings;
        private readonly IMapper _mapper;
        public SaveSettingsCommandHandeler(IApprovalSettings approvalSettings, IMapper mapper)
        {
            _approvalSettings = approvalSettings;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SaveSettings request, CancellationToken cancellationToken)
        {
            try
            {
                //var s = await _context.organizationApprovalSettings
                //.Where(i => i.OrganizationIdentifier == request.setting.OrganizationIdentifier
                //&& i.taskType == request.setting.taskType)
                //.Include(d => d.organizationApprovalSettingDetails).FirstOrDefaultAsync(cancellationToken);
                var s = await _approvalSettings.GetOrganizationApprovalSettingsByOrganizationIdentifierAndTaskType(request.setting.OrganizationIdentifier, request.setting.taskType);
                if (s == null)
                {
                    var set = _mapper.Map<ApprovalSettingDto, OrganizationApprovalSettings>(request.setting);
                    foreach(var setting in set.organizationApprovalSettingDetails)
                    {
                        if (!Enum.IsDefined(typeof(OrganizationApprovalStructure), setting.ManagerType))
                        {
                            throw new Exception("Manager Approval Type does not exists");
                        }
                        else if (setting.ManagerType == OrganizationApprovalStructure.Other && string.IsNullOrEmpty(setting.EmployeeIdentifier))
                        {
                            throw new Exception("For Non Manageral setting please add Employee Identifier");
                        }
                    }
                    //_context.organizationApprovalSettings.Add(set);
                    await _approvalSettings.AddOrganizationApprovalSettings(set);

                }
                else
                {
                    if(request.setting.approvalSettingDetails.Count > 0)
                    {
                        foreach (var setting in request.setting.approvalSettingDetails)
                        {
                            if(!Enum.IsDefined(typeof(OrganizationApprovalStructure), setting.ManagerType))
                            {
                                throw new Exception("Manager Approval Type does not exists");
                            }
                            else if (setting.ManagerType == OrganizationApprovalStructure.Other && string.IsNullOrEmpty(setting.EmployeeIdentifier))
                            {
                                throw new Exception("For Non Manageral setting please add Employee Identifier");
                            }                              
                        }
                        //var detail = await _context.organizationApprovalSettingDetails
                        //.Where(d => d.OrganizationApprovalSettingsId == s.Id)
                        //.ToListAsync(cancellationToken);
                        var detail = await _approvalSettings.GetOrganizationApprovalSettingsDetailsByOrganizationApprovalSettingsId(s.Id);
                        if (detail.ToList().Count > 0)
                            await _approvalSettings.DeleteOrganizationApprovalSettingsDetails(detail.ToList());
                        //_context.organizationApprovalSettingDetails.RemoveRange(detail);
                        
                        var set = _mapper.Map<List<ApprovalSettingDetailsDto>, List<OrganizationApprovalSettingDetail>>(request.setting.approvalSettingDetails);
                        s.organizationApprovalSettingDetails = set;
                        //_context.organizationApprovalSettings.Update(s);
                        await _approvalSettings.UpdateOrganizationApprovalSettings(s);

                    }
                    else
                    {
                        throw new Exception("Approval Structure Details can not be empty.");
                    }
                }
                //await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch(Exception e)
            {
              throw e;
            }
            //var s = _mapper.Map<ApprovalSettingDto, OrganizationApprovalSettings>(request.setting);

            
        }
    }
}
