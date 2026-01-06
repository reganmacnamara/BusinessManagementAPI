using AutoMapper;
using InvoiceAutomationAPI.Models;
using InvoiceAutomationAPI.UseCases.Clients.CreateClient;
using InvoiceAutomationAPI.UseCases.Clients.GetClient;
using InvoiceAutomationAPI.UseCases.Clients.GetClients;

namespace InvoiceAutomationAPI.Profiles
{

    public class ClientProfile : Profile
    {

        public ClientProfile()
        {
            CreateMap<Client, CreateClientResponse>();

            CreateMap<CreateClientRequest, Client>();

            CreateMap<List<Client>, GetClientsResponse>()
                .ForMember(response => response.Clients, output => output.MapFrom(source => source));

            CreateMap<Client, GetClientResponse>()
                .ForMember(response => response.Client, output => output.MapFrom(source => source));
        }

    }

}

