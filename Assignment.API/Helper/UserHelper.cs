using System.Security.Claims;

namespace Assignment.API.Helper
{
    public static class UserHelper
    {
        public static string GetUserId(ClaimsPrincipal userClaims)
        {
            var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
