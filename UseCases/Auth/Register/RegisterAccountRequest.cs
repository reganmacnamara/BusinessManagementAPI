using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using System.ComponentModel.DataAnnotations;

namespace MacsBusinessManagementAPI.UseCases.Auth.Register
{

    public class RegisterAccountRequest : IUseCaseRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }

}
