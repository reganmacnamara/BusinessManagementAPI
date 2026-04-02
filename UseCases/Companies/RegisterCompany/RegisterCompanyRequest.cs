using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Companies.RegisterCompany
{

    public class RegisterCompanyRequest : IUseCaseRequest
    {
        public string CompanyName { get; set; } = string.Empty;

        public string CompanyABN { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string AddressLine1 { get; set; } = string.Empty;

        public string AddressLine2 { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string PostCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;
    }

}
