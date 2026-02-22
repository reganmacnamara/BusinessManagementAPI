using AutoMapper;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem;

namespace BusinessManagementAPI.Profiles;

public class RecieptItemProfile : Profile
{
    public RecieptItemProfile()
    {
        CreateMap<UpsertReceiptItemRequest, ReceiptItem>()
            .ForMember(d => d.Invoice, o => o.Ignore())
            .ForMember(d => d.Receipt, o => o.Ignore());
    }
}
