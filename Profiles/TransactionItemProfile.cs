using AutoMapper;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.UseCases.TransactionItems.CreateTransactionItem;
using BusinessManagementAPI.UseCases.TransactionItems.GetTransactionItems;

namespace BusinessManagementAPI.Profiles;

public class TransactionItemProfile : Profile
{
    public TransactionItemProfile()
    {
        CreateMap<CreateTransactionItemRequest, TransactionItem>();

        CreateMap<TransactionItem, CreateTransactionItemResponse>();

        CreateMap<List<TransactionItem>, GetTransactionItemsResponse>()
                .ForMember(response => response.TransactionItems, output => output.MapFrom(source => source));
    }
}
