using AutoMapper;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Invoices.CreateInvoice;

namespace MacsBusinessManagementAPI.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            _ = CreateMap<CreateInvoiceRequest, Invoice>()
                .ForMember(d => d.Outstanding, o => o.Ignore())
                .ForMember(d => d.Client, o => o.Ignore())
                .ForMember(d => d.DueDate, o => o.Ignore());

            _ = CreateMap<Invoice, CreateInvoiceResponse>();
        }
    }
}
