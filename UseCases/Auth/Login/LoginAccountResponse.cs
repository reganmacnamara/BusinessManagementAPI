namespace MacsBusinessManagementAPI.UseCases.Auth.Login
{

    public class LoginAccountResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiry { get; set; }
    }

}
