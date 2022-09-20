using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Commands.SaveCurrencies
{
    public class SaveCurrenciesCommand : IRequest
    {
        public List<CurrencyDetail> currencyDetails { get; set; }
    }
    public class CurrencyDetail
    {
        public string businessIdentifier { get; set; }
        public string currencycode { get; set; }
        public string currencyName { get; set; }
    }

    public class SaveCurrenciesCommandHandeler : IRequestHandler<SaveCurrenciesCommand>
    {
        private readonly IApplicationDbContext _context;
        public SaveCurrenciesCommandHandeler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(SaveCurrenciesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string[] businesses = request.currencyDetails.Select(c => c.businessIdentifier).ToArray();
                var b = await _context.Businesses
                    .Where(b => businesses.Contains(b.BusinessIdentifier))
                    .ToListAsync();
                
                foreach(var p in b)
                {
                    var bus = request.currencyDetails
                        .FirstOrDefault(b => b.businessIdentifier == p.BusinessIdentifier);
                    p.CurrencyCode = bus.currencycode;
                    p.CurrencyName = bus.currencyName;
                }
                _context.Businesses.UpdateRange(b);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
