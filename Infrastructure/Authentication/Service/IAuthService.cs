namespace MacsBusinessManagementAPI.Infrastructure.Authentication.Service
{

    public interface IAuthService
    {
        public string GenerateToken(long accountId, string email);
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hash);
    }

}
