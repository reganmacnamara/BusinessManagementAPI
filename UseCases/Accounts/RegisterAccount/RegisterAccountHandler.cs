using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Services;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Accounts.RegisterAccount;

public class RegisterAccountHandler(IAccountService accountService, IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> RegisterAccount(RegisterAccountRequest request)
    {
        var _Account = new Account()
        {
            Email = request.Email,
            Password = request.Password,
        };

        if (_Account.Password.Length <= 5)
            return Results.BadRequest("Password must be longer than 5 characters.");

        var _EmailIsUsed = m_Context.GetEntities<Account>()
            .Any(a => a.Email == _Account.Email);

        if (_EmailIsUsed)
            return Results.BadRequest("Email already in use.");

        _Account.IsActive = true;
        _Account.CreatedDate = DateTime.Now;

        m_Context.Accounts.Add(_Account);

        _ = await m_Context.SaveChangesAsync();

        await accountService.RegisterAccount(_Account);

        return Results.Created(string.Empty, _Account.AccountID);
    }
}
