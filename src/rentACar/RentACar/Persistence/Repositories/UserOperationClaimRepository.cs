using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, int, BaseDbContext>, IUserOperationClaimRepository
{
    public UserOperationClaimRepository(BaseDbContext context) : base(context) { }

    public async Task<IList<OperationClaim>> GetOperationClaimsByUserIdAsync(int userId)
    {
        // İlgili kullanıcıya ait operationClaim değerlerine ulaşıyoruz
        List<OperationClaim> operationClaims = await Query()
            .AsNoTracking()
            .Where(u => u.UserId == userId)
            .Select(u => new OperationClaim { Id = u.OperationClaimId, Name = u.OperationClaim.Name})
            .ToListAsync();
        return operationClaims;
    }
}