using AutoMapper;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Receipts.CreateReceipt;

namespace BusinessManagementAPI.Profiles
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
