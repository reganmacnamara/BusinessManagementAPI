using AutoMapper;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Clients.UpdateClient
{

    public class UpdateClientHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<UpdateClientResponse> UpdateClient(UpdateClientRequest request)
        {
            var _Client = await m_Context.Clients.FindAsync(request.ClientId);

            if (_Client is not null)
            {
                var _RequestProperties = request.GetType().GetProperties();
                var _ClientProperties = _Client.GetType().GetProperties();

                foreach (var property in _RequestProperties)
                {
                    var targetProperty = _ClientProperties.FirstOrDefault(p =>
                        p.Name == property.Name &&
                        p.PropertyType == property.PropertyType &&
                        p.CanWrite &&
                        p.Name != "ClientID");

                    if (targetProperty != null)
                    {
                        var value = property.GetValue(request, null);
                        targetProperty.SetValue(_Client, value, null);
                    }
                }

                await m_Context.SaveChangesAsync();

                var _Response = new UpdateClientResponse()
                {
                    ClientID = _Client.ClientID
                };

                return _Response;
            }

            return new UpdateClientResponse();
        }
    }

}
