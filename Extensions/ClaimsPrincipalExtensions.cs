using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MacsBusinessManagementAPI.Extensions
{

    public static class ClaimsPrincipalExtensions
    {
        public static long GetAccountID(this ClaimsPrincipal user)
        {
            var _Claim = user.FindFirst(JwtRegisteredClaimNames.Sub)
                      ?? user.FindFirst(ClaimTypes.NameIdentifier);

            return _Claim is not null && long.TryParse(_Claim.Value, out var accountID)
                ? accountID
                : 0;
        }

        public static long GetCompanyID(this ClaimsPrincipal user)
        {
            var _Claim = user.FindFirst("companyID");

            return _Claim is not null && long.TryParse(_Claim.Value, out var companyID)
                ? companyID
                : 0;
        }
    }

}
