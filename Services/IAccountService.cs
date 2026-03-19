using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.Services;

public interface IAccountService
{
    Task ActivateAccount(Account account);

    Task DeActivateAccount(Account account);

    Task DeleteAccount(Account account);

    Task Login(Account account);

    Task RegisterAccount(Account account);
}
