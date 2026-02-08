using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Clients.UpdateClient
{

    public class UpdateClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> UpdateClient(UpdateClientRequest request)
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

                return Results.Ok(_Response);
            }
            else
                return Results.NotFound("Client was not found.");
        }
    }

}
