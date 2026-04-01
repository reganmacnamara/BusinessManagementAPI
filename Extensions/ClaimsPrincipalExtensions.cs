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

            if (_Claim is null || !long.TryParse(_Claim.Value, out var accountNumber))
                throw new UnauthorizedAccessException("AccountID claim is missing or invalid.");

            return accountNumber;
        }
    }

}
