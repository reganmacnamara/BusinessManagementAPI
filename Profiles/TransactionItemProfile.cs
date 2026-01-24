using AutoMapper;
using InvoiceAutomationAPI.Models;
using InvoiceAutomationAPI.UseCases.TransactionItems.CreateTransactionItem;

namespace InvoiceAutomationAPI.Profiles;

public class TransactionItemProfile : Profile
{
    public TransactionItemProfile()
    {
        CreateMap<CreateTransactionItemRequest, TransactionItem>();
    }
}
