using AutoMapper;
using InvoiceAutomationAPI.Models;
using InvoiceAutomationAPI.UseCases.TransactionItems.CreateTransactionItem;
using InvoiceAutomationAPI.UseCases.TransactionItems.GetTransactionItems;

namespace InvoiceAutomationAPI.Profiles;

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
