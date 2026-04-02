namespace MacsBusinessManagementAPI.Infrastructure.Authentication
{

    public interface ITenantProvider
    {
        long CompanyID { get; }
    }

}
