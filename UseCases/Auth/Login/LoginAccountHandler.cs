using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.Infrastructure.Services.Auth;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Auth.Login
{

    public class LoginAccountHandler(IAuthService authService, SQLContext context) : IUseCaseHandler<LoginAccountRequest>
    {
        public async Task<IResult> HandleAsync(LoginAccountRequest request, CancellationToken cancellationToken)
        {
            var _Account = await context.GetEntities<Account>()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => a.Email.ToLower() == request.Email.ToLower() && a.IsActive, cancellationToken);

            if (_Account is null || !authService.VerifyPassword(request.Password, _Account.Password))
                return Results.Unauthorized();

            _Account.LastLoginDate = DateTime.UtcNow;

            _ = await context.SaveChangesAsync(cancellationToken);

            var token = authService.GenerateToken(_Account.AccountID, _Account.CompanyID ?? 0, _Account.Email);

            return Results.Ok(new LoginAccountResponse()
            {
                Token = token,
                Expiry = DateTime.UtcNow.AddMinutes(60)
            });
        }
    }

}
