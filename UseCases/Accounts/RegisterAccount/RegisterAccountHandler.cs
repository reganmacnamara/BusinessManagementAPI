using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.Services;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Accounts.RegisterAccount;

public class RegisterAccountHandler(IAccountService accountService, IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> RegisterAccount(RegisterAccountRequest request)
    {
        var _Account = new Account()
        {
            Email = request.Email,
            Password = request.Password,
        };

        var _EmailIsUsed = context.GetEntities<Account>()
            .Any(a => a.Email == _Account.Email);

        if (_EmailIsUsed)
            return Results.Conflict("Email already in use.");

        _Account.IsActive = true;
        _Account.CreatedDate = DateTime.Now;

        context.Accounts.Add(_Account);

        _ = await context.SaveChangesAsync();

        await accountService.RegisterAccount(_Account);

        return Results.Created(string.Empty, _Account.AccountID);
    }
}
