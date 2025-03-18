using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class RefreshToken : Entity<int> 
{
    // AccessToken'ın süresi dolmuşsa ve yeniden alınması gerektiğinde kullanılan anahtar.

    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string CreatedByIp { get; set; }
    public DateTime? Revoked { get; set; } // İşlemi iptal etme
    public string? RevokedByIp { get; set; }
    public string? ReplacedByToken { get; set; } // Hangi token ile replace edildi
    public string? ReasonRevoked { get; set; } // İptal edilme sebebi

    public virtual User User { get; set; } = null!;


    public RefreshToken()
    {
        Token = string.Empty;
        CreatedByIp = string.Empty;
    }

    public RefreshToken(int userId, string token, DateTime expires, string createdByIp)
    {
        UserId = userId;
        Token = token;
        Expires = expires;
        CreatedByIp = createdByIp;
    }

    public RefreshToken(int id, int userId, string token, DateTime expires, string createdByIp) :base(id)
    {
        UserId = userId;
        Token = token;
        Expires = expires;
        CreatedByIp = createdByIp;
    }
}