using System.Security.Claims;

namespace Core.Security.Extensions;

public static class ClaimPrincipalExtension
{
    // ClaimsPrincipal → Kullanıcının kimlik ve yetkilendirme bilgilerini içeren bir nesnedir.

    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType) =>
        claimsPrincipal?.FindAll(claimType)?.Select(c => c.Value).ToList();


    // Kullanıcının sahip olduğu rolleri döndürme
    public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal) => 
        claimsPrincipal?.Claims(ClaimTypes.Role);

    
    // Kullanıcının ID bilgisini almaya yarar. Aldığımız id bilgisi sayesinde kullanıcının datasını döndürebileceğiz
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal) =>
        Convert.ToInt32(claimsPrincipal?.Claims(ClaimTypes.NameIdentifier)?.FirstOrDefault());
}