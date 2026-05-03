using AutoMapper;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Services.CreateService;
using MacsBusinessManagementAPI.UseCases.Services.UpsertServiceActivity;

namespace MacsBusinessManagementAPI.Profiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<CreateServiceRequest, Service>();

        CreateMap<UpsertServiceActivityRequest, ServiceActivity>();
    }
}
