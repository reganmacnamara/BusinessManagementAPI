using AutoMapper;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Receipts.CreateReceipt;

namespace MacsBusinessManagementAPI.Profiles
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            _ = CreateMap<CreateReceiptRequest, Receipt>()
                .ForMember(d => d.Outstanding, o => o.Ignore())
                .ForMember(d => d.Client, o => o.Ignore());

            _ = CreateMap<Receipt, CreateReceiptResponse>();
        }
    }
}
