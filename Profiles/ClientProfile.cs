using AutoMapper;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Clients.CreateClient;
using MacsBusinessManagementAPI.UseCases.Clients.GetClient;
using MacsBusinessManagementAPI.UseCases.Clients.GetClients;
using MacsBusinessManagementAPI.UseCases.Clients.UpdateClient;

namespace MacsBusinessManagementAPI.Profiles
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

