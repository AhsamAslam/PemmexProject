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

namespace TaskManager.API.Queries.GetHistoryTasksByManagerId
{
    public class GetNotApprovedTasksQuery : IRequest<List<TaskDto>>
    {
        public string Id { get; set; }
    }
    public class GetHistoryTasksByManagerIdQueryHandeler : IRequestHandler<GetNotApprovedTasksQuery, List<TaskDto>>
    {
        private readonly IApprovalSettings _approvalSettings;
        private readonly IMapper _mapper;

        public GetHistoryTasksByManagerIdQueryHandeler(IApprovalSettings approvalSettings, IMapper mapper)
        {
            _approvalSettings = approvalSettings;
            _mapper = mapper;
        }
        public async Task<List<TaskDto>> Handle(GetNotApprovedTasksQuery request, CancellationToken cancellationToken)
        {
            //var Identifiers = await (from b in _context.BaseTasks
            //                         where b.RequestedByIdentifier == request.Id && b.isActive == true
            //                         && (b.currentTaskStatus == TaskStatuses.Declined 
            //                         || b.currentTaskStatus == TaskStatuses.Cancelled)
            //                         select b.TaskIdentifier
            //                ).Distinct().ToListAsync();
            var Identifiers = await _approvalSettings.GetBaseTaskAndByRequestedByIdentifierAndStatuses(request.Id, (int)TaskStatuses.Declined, (int)TaskStatuses.Cancelled);


            //var tasks = await _context.BaseTasks
            //    .Include(h => h.ChangeHoliday)
            //    .Include(t => t.ChangeTitle)
            //    .Include(m => m.ChangeManager)
            //    .Include(c => c.ChangeCompensation)
            //    .Include(g => g.ChangeGrade)
            //    .Include(g => g.ChangeTeam)
            //    .Include(g => g.ChangeBonus)
            //    .Where(t => Identifiers.Contains(t.TaskIdentifier) && (t.currentTaskStatus == TaskStatuses.Declined || t.currentTaskStatus == TaskStatuses.Cancelled)).ToListAsync();
            var Id = Identifiers.Select(x => x.TaskIdentifier);
            var tasks = await _approvalSettings.GetAllBaseTaskByTaskIdentifierAndStatuses(Id.ToArray(), (int)TaskStatuses.Declined, (int)TaskStatuses.Cancelled);
            return _mapper.Map<List<BaseTask>, List<TaskDto>>(tasks);
        }
    }
}
