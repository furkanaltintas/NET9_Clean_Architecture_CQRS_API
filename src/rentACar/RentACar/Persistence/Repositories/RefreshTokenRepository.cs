using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, int, BaseDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BaseDbContext context) : base(context) { }

    public async Task<List<RefreshToken>> GetOldRefreshTokenAsync(int userId, int refreshTokenTTL)
    {
        List<RefreshToken> refreshTokens = await Query()
            .AsNoTracking()
            .Where(
            r =>
            r.UserId == userId
            && r.Revoked == null // iptal bilgisi var ise eski bir refresh token olduğu anlamına gelir
            && r.Expires >= DateTime.UtcNow
            && r.CreatedDate.AddDays(refreshTokenTTL) <= DateTime.UtcNow
            ).ToListAsync();

        return refreshTokens;
    }
}
