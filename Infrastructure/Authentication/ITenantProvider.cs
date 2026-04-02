namespace MacsBusinessManagementAPI.Infrastructure.Authentication
{

    public interface ITenantProvider
    {
        long AccountID { get; }
        long CompanyID { get; }
    }

}
