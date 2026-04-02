using MacsBusinessManagementAPI.Extensions;

namespace MacsBusinessManagementAPI.Infrastructure.Authentication
{
    public class TenantProvider(IHttpContextAccessor contextAccessor) : ITenantProvider
    {
        public long AccountID => contextAccessor.HttpContext?.User.GetAccountID() ?? 0;
        public long CompanyID => contextAccessor.HttpContext?.User.GetCompanyID() ?? 0;
    }
}
