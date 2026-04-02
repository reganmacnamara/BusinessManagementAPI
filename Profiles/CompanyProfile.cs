using AutoMapper;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Companies.RegisterCompany;

namespace MacsBusinessManagementAPI.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            _ = CreateMap<RegisterCompanyRequest, Company>()
                .ForMember(d => d.Accounts, o => o.Ignore())
                .ForMember(d => d.Clients, o => o.Ignore())
                .ForMember(d => d.Invoices, o => o.Ignore())
                .ForMember(d => d.PaymentTerms, o => o.Ignore())
                .ForMember(d => d.Products, o => o.Ignore())
                .ForMember(d => d.Receipts, o => o.Ignore());
        }
    }
}
