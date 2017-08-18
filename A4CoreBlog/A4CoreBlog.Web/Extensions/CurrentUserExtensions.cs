using System.Security.Claims;

namespace A4CoreBlog.Web
{
    public static class CurrentUserExtensions
    {
        /// <summary>
        /// User ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            ClaimsPrincipal currentUser = user;
            return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static string GetJwtTokenUserName(this ClaimsPrincipal user)
        {
            return user.GetUserId();
        }
    }
}
