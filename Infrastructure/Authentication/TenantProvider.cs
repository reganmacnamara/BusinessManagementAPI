using MacsBusinessManagementAPI.Extensions;

namespace MacsBusinessManagementAPI.Infrastructure.Authentication
{
    public class TenantProvider(IHttpContextAccessor contextAccessor) : ITenantProvider
    {
        public long CompanyID => contextAccessor.HttpContext?.User.GetCompanyID() ?? 0;
    }
}
