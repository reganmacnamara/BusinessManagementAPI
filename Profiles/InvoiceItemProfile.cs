using AutoMapper;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem;

namespace MacsBusinessManagementAPI.Profiles;

public class InvoiceItemProfile : Profile
{
    public InvoiceItemProfile()
    {
        CreateMap<UpsertInvoiceItemRequest, InvoiceItem>()
            .ForMember(d => d.Product, o => o.Ignore())
            .ForMember(d => d.Service, o => o.Ignore());
    }
}
