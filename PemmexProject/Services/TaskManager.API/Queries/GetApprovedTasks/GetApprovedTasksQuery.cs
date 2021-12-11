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
using TaskManager.API.Dtos;
using TaskManager.API.Enumerations;

namespace TaskManager.API.Queries.GetCurrentTasksByManagerId
{
    public class GetApprovedTasksQuery : IRequest<List<TaskDto>>
    {
        public string Id { get; set; }
    }
    public class GetApprovedTasksQueryHandeler : IRequestHandler<GetApprovedTasksQuery, List<TaskDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetApprovedTasksQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TaskDto>> Handle(GetApprovedTasksQuery request, CancellationToken cancellationToken)
        {
            var Identifiers = await (from b in _context.BaseTasks
                                     where b.RequestedByIdentifier == request.Id && b.isActive == true
                                     && b.currentTaskStatus == TaskStatuses.Approved
                                     select b.TaskIdentifier
                             ).Distinct().ToListAsync();

            var tasks = await _context.BaseTasks
                .Include(h => h.ChangeHoliday)
                .Include(t => t.ChangeTitle)
                .Include(m => m.ChangeManager)
                .Include(c => c.ChangeCompensation)
                .Include(g => g.ChangeGrade)
                .Include(g => g.ChangeTeam)
                .Where(t => Identifiers.Contains(t.TaskIdentifier) && (t.currentTaskStatus == TaskStatuses.Approved)).ToListAsync();
            return _mapper.Map<List<BaseTask>, List<TaskDto>>(tasks);
        }
    }
}
