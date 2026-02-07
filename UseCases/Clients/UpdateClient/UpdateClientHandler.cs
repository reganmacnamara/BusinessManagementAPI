using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Clients.UpdateClient
{

    public class UpdateClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<UpdateClientResponse> UpdateClient(UpdateClientRequest request)
        {
            var _Client = await m_Context.Clients.FindAsync(request.ClientId);

            if (_Client is not null)
            {
                _Client = UpdateEntityFromRequest(_Client, request, ["ClientID"]);

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
