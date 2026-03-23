namespace MacsBusinessManagementAPI.UseCases.Auth.Register
{

    public class RegisterAccountResponse
    {
        public long AccountID { get; set; }
        public string Email { get; set; } = string.Empty;
    }

}
