using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.UseCases.Companies.GetCompany
{

    public class GetCompanyResponse
    {
        public Company Company { get; set; } = new();
    }

}
