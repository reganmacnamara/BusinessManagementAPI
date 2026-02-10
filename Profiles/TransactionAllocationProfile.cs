using AutoMapper;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.TransactionAllocations.CreateTransactionAllocation;

namespace BusinessManagementAPI.Profiles
{
    public class TransactionAllocationProfile : Profile
    {
        public TransactionAllocationProfile()
        {
            CreateMap<CreateTransactionAllocationRequest, TransactionAllocation>();
        }
    }
}
