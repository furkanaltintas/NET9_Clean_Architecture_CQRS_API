using Core.Security.Entities;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    // Kullanıcının user datası ve o kullanıcıya ait claimleri
    AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);

    RefreshToken CreateRefreshToken(User user, string ipAddress);
}