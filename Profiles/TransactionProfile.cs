using AutoMapper;
using InvoiceAutomationAPI.Models;
using InvoiceAutomationAPI.UseCases.Transactions.CreateTransaction;
using InvoiceAutomationAPI.UseCases.Transactions.GetClientTransactions;
using InvoiceAutomationAPI.UseCases.Transactions.GetTransaction;
using InvoiceAutomationAPI.UseCases.Transactions.GetTransactions;

namespace InvoiceAutomationAPI.Profiles
{
    public class TransactionProfile : Profile
    {

        public TransactionProfile()
        {
            CreateMap<CreateTransactionRequest, Transaction>();

            CreateMap<Transaction, CreateTransactionResponse>();

            CreateMap<Transaction, GetTransactionResponse>()
                .ForMember(response => response.Transaction, output => output.MapFrom(source => source))
                .ForMember(response => response.TransactionItems, output => output.Ignore());

            CreateMap<List<Transaction>, GetTransactionsResponse>()
                .ForMember(response => response.Transactions, output => output.MapFrom(source => source));

            CreateMap<List<Transaction>, GetClientTransactionsResponse>()
                .ForMember(response => response.Transactions, output => output.MapFrom(source => source));
        }

    }
}
