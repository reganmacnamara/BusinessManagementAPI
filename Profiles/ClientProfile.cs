using AutoMapper;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.UseCases.Clients.CreateClient;
using BusinessManagementAPI.UseCases.Clients.GetClient;
using BusinessManagementAPI.UseCases.Clients.GetClients;
using BusinessManagementAPI.UseCases.Clients.UpdateClient;

namespace BusinessManagementAPI.Profiles
{

    public class ClientProfile : Profile
    {

        public ClientProfile()
        {
            CreateMap<Client, CreateClientResponse>();

            CreateMap<CreateClientRequest, Client>();

            CreateMap<UpdateClientRequest, Client>();

            CreateMap<List<Client>, GetClientsResponse>()
                .ForMember(response => response.Clients, output => output.MapFrom(source => source));

            CreateMap<Client, GetClientResponse>()
                .ForMember(response => response.Client, output => output.MapFrom(source => source));
        }

    }

}

