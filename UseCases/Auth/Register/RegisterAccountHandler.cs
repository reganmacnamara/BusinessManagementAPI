using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Authentication.Service;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Auth.Register
{

    public class RegisterAccountHandler(IAuthService authService, SQLContext context) : IUseCaseHandler<RegisterAccountRequest>
    {
        public async Task<IResult> HandleAsync(RegisterAccountRequest request, CancellationToken cancellationToken)
        {
            var _EmailInUse = await context.GetEntities<Account>()
                .AnyAsync(a => a.Email.ToLower() == request.Email.ToLower(), cancellationToken);

            if (_EmailInUse)
                return Results.Conflict("An account with this email already exists.");

            var account = new Account
            {
                Email = request.Email,
                Password = authService.HashPassword(request.Password),
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            context.Accounts.Add(account);

            _ = await context.SaveChangesAsync(cancellationToken);

            return Results.Created(string.Empty, new RegisterAccountResponse()
            {
                AccountID = account.AccountID,
                Email = account.Email,
            });
        }
    }

}
