using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.context;

namespace TaskManager.API.Queries.GetAspTeamSummary
{
    //public class GetAspTeamSummaryQuery : IRequest
    //{
    //    public string Id { get; set; }
    //}
    //public class GetAspTeamSummaryQueryHandeler : IRequestHandler<GetAspTeamSummaryQuery>
    //{
    //    private readonly IApplicationDbContext _context;
    //    private readonly IMapper _mapper;

    //    public GetAspTeamSummaryQueryHandeler(IApplicationDbContext context, IMapper mapper)
    //    {
    //        _context = context;
    //        _mapper = mapper;
    //    }
    //    public async Task<Unit> Handle(GetAspTeamSummaryQuery request, CancellationToken cancellationToken)
    //    {
    //        var tasks = await _context.BaseTasks
    //            .Include(g => g.ChangeBudgetPromotion)
    //            .ThenInclude(h => h.changeBudgetPromotionDetails)
    //            .Where(t => Identifiers.Contains(t.TaskIdentifier) && (t.currentTaskStatus == TaskStatuses.Pending)).ToListAsync();


    //        return _mapper.Map<List<BaseTask>, List<TaskDto>>(tasks);
    //    }
    //}
}
