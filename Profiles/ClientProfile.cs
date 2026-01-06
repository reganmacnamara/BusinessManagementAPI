using AutoMapper;
using InvoiceAutomationAPI.Models;
using InvoiceAutomationAPI.UseCases.Clients.CreateClient;

namespace InvoiceAutomationAPI.Profiles
{

    public class ClientProfile : Profile
    {

        public ClientProfile()
        {
            CreateMap<Client, CreateClientResponse>();
        }

    }

}

