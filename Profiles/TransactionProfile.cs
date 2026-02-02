using AutoMapper;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.UseCases.Transactions.CreateTransaction;
using BusinessManagementAPI.UseCases.Transactions.GetClientTransactions;
using BusinessManagementAPI.UseCases.Transactions.GetTransaction;
using BusinessManagementAPI.UseCases.Transactions.GetTransactions;

namespace BusinessManagementAPI.Profiles
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
