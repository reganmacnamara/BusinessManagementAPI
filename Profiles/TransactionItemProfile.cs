using AutoMapper;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.TransactionItems.GetTransactionItems;
using BusinessManagementAPI.UseCases.TransactionItems.UpsertTransactionItem;

namespace BusinessManagementAPI.Profiles;

public class TransactionItemProfile : Profile
{
    public TransactionItemProfile()
    {
        CreateMap<UpsertTransactionItemRequest, TransactionItem>();

        CreateMap<TransactionItem, UpsertTransactionItemResponse>();

        CreateMap<List<TransactionItem>, GetTransactionItemsResponse>()
                .ForMember(response => response.TransactionItems, output => output.MapFrom(source => source));
    }
}
