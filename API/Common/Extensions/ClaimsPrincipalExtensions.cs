using System.Security.Claims;
using Domain.Constants;
using Domain.Enums;

namespace API.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirstValue(CustomClaimTypes.Id));
        }
        public static ERole GetRole(this ClaimsPrincipal user)
        {
            return (ERole) int.Parse(user.FindFirstValue(ClaimTypes.Role));
        }
        public static string GetUserName(this ClaimsPrincipal user)
        {
            // claims is null, when http context doesn't exist.
            if (user == null)
            {
                return "system";
            }
            // claims doesn't contain UserName, when connection is anonymous
            return user.FindFirstValue(CustomClaimTypes.UserName) ?? "guest";
        }
    }
}
