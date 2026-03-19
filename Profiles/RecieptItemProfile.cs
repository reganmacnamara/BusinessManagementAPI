using AutoMapper;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem;

namespace MacsBusinessManagementAPI.Profiles;

public class RecieptItemProfile : Profile
{
    public RecieptItemProfile()
    {
        CreateMap<UpsertReceiptItemRequest, ReceiptItem>()
            .ForMember(d => d.Invoice, o => o.Ignore())
            .ForMember(d => d.Receipt, o => o.Ignore());
    }
}
