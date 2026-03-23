using System.ComponentModel.DataAnnotations;

namespace MacsBusinessManagementAPI.UseCases.Auth.Login
{

    public class LoginAccountRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }

}
