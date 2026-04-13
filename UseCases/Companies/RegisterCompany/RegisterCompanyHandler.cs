using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ABNValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.Infrastructure.Services.Auth;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Companies.RegisterCompany
{

    public class RegisterCompanyHandler(IAuthService authService, IMapper mapper, SQLContext context) : IUseCaseHandler<UpdateCompanyRequest>
    {
        public async Task<IResult> HandleAsync(UpdateCompanyRequest request, CancellationToken cancellationToken)
        {
            var _ABNIsValid = ABNValidator.IsValidABN(request.CompanyABN);

            if (!_ABNIsValid)
                return Results.BadRequest("ABN is not valid.");

            var _ABNRegistered = await context.GetEntities<Company>()
                .IgnoreQueryFilters()
                .AnyAsync(c => c.CompanyABN == request.CompanyABN, cancellationToken);

            if (_ABNRegistered)
                return Results.Conflict("ABN has already been registered");

            var _Account = await context.GetEntities<Account>()
                .IgnoreQueryFilters()
                .SingleAsync(a => a.AccountID == context.AccountID, cancellationToken);

            var _Company = mapper.Map<Company>(request);

            _Company.Accounts.Add(_Account);

            _Company.CompanySettings = new();

            context.Companies.Add(_Company);

            _ = await context.SaveChangesAsync(cancellationToken);

            var _Token = authService.GenerateToken(_Account.AccountID, _Company.CompanyID, _Account.Email);

            var _Response = new RegisterCompanyResponse()
            {
                CompanyID = _Company.CompanyID,
                Token = _Token,
                Expiry = DateTime.UtcNow.AddMinutes(60)
            };

            return Results.Created("", _Response);
        }
    }

}
