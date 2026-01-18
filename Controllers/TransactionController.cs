using AutoMapper;
using InvoiceAutomationAPI.UseCases.Transactions.CreateTransaction;
using InvoiceAutomationAPI.UseCases.Transactions.DeleteTransaction;
using InvoiceAutomationAPI.UseCases.Transactions.GetClientTransactions;
using InvoiceAutomationAPI.UseCases.Transactions.GetTransaction;
using InvoiceAutomationAPI.UseCases.Transactions.GetTransactions;
using InvoiceAutomationAPI.UseCases.Transactions.UpdateTransaction;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAutomationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController(IMapper mapper) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IResult> CreateTransaction([FromBody] CreateTransactionRequest request)
        {
            var handler = new CreateTransactionHandler(mapper);

            var _Response = await handler.CreateTransaction(request);

            return _Response is not null
                ? Results.Ok(_Response)
                : Results.BadRequest();
        }

        [HttpPost("Delete")]
        public async Task<IResult> DeleteTransaction([FromBody] DeleteTransactionRequest request)
        {
            var handler = new DeleteTransactionHandler(mapper);

            await handler.DeleteTransaction(request);

            return Results.NoContent();
        }

        [HttpGet("Client/{clientID}")]
        public async Task<IResult> GetClientTransactions([FromRoute] long clientID)
        {
            var _Request = new GetClientTransactionsRequest { ClientID = clientID };

            var handler = new GetClientTransactionsHandler(mapper);

            var _Response = await handler.GetClientTransactions(_Request);

            return Results.Ok(_Response);
        }

        [HttpGet("{transactionID}")]
        public async Task<IResult> GetTransaction([FromRoute] long transactionID)
        {
            var handler = new GetTransactionHandler(mapper);

            var _Request = new GetTransactionRequest()
            {
                TransactionID = transactionID
            };

            var _Response = await handler.GetTransaction(_Request);

            return _Response is not null
                ? Results.Ok(_Response)
                : Results.BadRequest();
        }

        [HttpGet]
        public async Task<IResult> GetTransactions()
        {
            var handler = new GetTransactionsHandler(mapper);

            var _Response = await handler.GetTransactions();

            return Results.Ok(_Response);
        }

        [HttpPost("Update")]
        public async Task<IResult> UpdateTransaction([FromBody] UpdateTransactionRequest request)
        {
            var handler = new UpdateTransactionHandler(mapper);

            var _Result = await handler.UpdateTransaction(request);

            return _Result.TransactionId != 0
                ? Results.Ok(_Result)
                : Results.NotFound();
        }
    }
}
