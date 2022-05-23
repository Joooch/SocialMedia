using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace SocialMedia.API.Extensions
{
    public static class IdentityExtensions
    {
        public static Guid GetUserId(this IIdentity identity)
        {
            if (identity.Name == null)
            {
                throw new ArgumentNullException("Invalid userid");
            }

            return new Guid(identity.Name);
        }

        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(AuthOptions.ClientClaimId)!.Value;
            return new Guid(userId);
        }
    }
}
