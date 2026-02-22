using AutoMapper;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem;

namespace BusinessManagementAPI.Profiles;

public class InvoiceItemProfile : Profile
{
    public InvoiceItemProfile()
    {
        CreateMap<UpsertInvoiceItemRequest, InvoiceItem>()
            .ForMember(d => d.Invoice, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());
    }
}
