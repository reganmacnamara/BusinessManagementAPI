namespace MacsBusinessManagementAPI.Infrastructure.Authentication.Service
{

    public interface IAuthService
    {
        string GenerateToken(long accountId, string email);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

}
