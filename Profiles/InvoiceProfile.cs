using AutoMapper;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Invoices.CreateInvoice;

namespace BusinessManagementAPI.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            _ = CreateMap<CreateInvoiceRequest, Invoice>()
                .ForMember(d => d.Outstanding, o => o.Ignore())
                .ForMember(d => d.Client, o => o.Ignore());
        }
    }
}
