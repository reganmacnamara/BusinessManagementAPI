using AutoMapper;
using InvoiceAutomationAPI.UseCases.Transactions.CreateTransaction;
using InvoiceAutomationAPI.UseCases.Transactions.DeleteTransaction;
using InvoiceAutomationAPI.UseCases.Transactions.GetTransaction;
using InvoiceAutomationAPI.UseCases.Transactions.GetTransactions;
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
    }
}
