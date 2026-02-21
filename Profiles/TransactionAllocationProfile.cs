using AutoMapper;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.TransactionAllocations.UpsertTransactionAllocation;
namespace BusinessManagementAPI.Profiles
{
    public class TransactionAllocationProfile : Profile
    {
        public TransactionAllocationProfile()
        {
            CreateMap<UpsertTransactionAllocationRequest, TransactionAllocation>();
        }
    }
}
