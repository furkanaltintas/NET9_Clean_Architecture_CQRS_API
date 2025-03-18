using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken, int>, IRepository<RefreshToken, int>
{
    // Kullanıcının eski RefreshToken değerlerine ulaşmak için
    Task<List<RefreshToken>> GetOldRefreshTokenAsync(int userId, int refreshTokenTTL);
}