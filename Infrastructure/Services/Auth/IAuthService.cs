namespace MacsBusinessManagementAPI.Infrastructure.Services.Auth
{

    public interface IAuthService
    {
        public string GenerateToken(long accountID, long companyID, string email);
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hash);
    }

}
