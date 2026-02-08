using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Transactions.CreateTransaction;
using BusinessManagementAPI.UseCases.Transactions.DeleteTransaction;
using BusinessManagementAPI.UseCases.Transactions.GetClientTransactions;
using BusinessManagementAPI.UseCases.Transactions.GetTransaction;
using BusinessManagementAPI.UseCases.Transactions.GetTransactions;
using BusinessManagementAPI.UseCases.Transactions.UpdateTransaction;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController(IMapper mapper, SQLContext context) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IResult> CreateTransaction([FromBody] CreateTransactionRequest request)
        {
            var handler = new CreateTransactionHandler(mapper, context);

            var _Response = await handler.CreateTransaction(request);

            return _Response;
        }

        [HttpPost("Delete")]
        public async Task<IResult> DeleteTransaction([FromBody] DeleteTransactionRequest request)
        {
            var handler = new DeleteTransactionHandler(mapper, context);

            var _Response = await handler.DeleteTransaction(request);

            return _Response;
        }

        [HttpGet("Client/{clientID}")]
        public async Task<IResult> GetClientTransactions([FromRoute] long clientID)
        {
            var _Request = new GetClientTransactionsRequest { ClientID = clientID };

            var handler = new GetClientTransactionsHandler(mapper, context);

            var _Response = await handler.GetClientTransactions(_Request);

            return _Response;
        }

        [HttpGet("{transactionID}")]
        public async Task<IResult> GetTransaction([FromRoute] long transactionID)
        {
            var handler = new GetTransactionHandler(mapper, context);

            var _Request = new GetTransactionRequest()
            {
                TransactionID = transactionID
            };

            var _Response = await handler.GetTransaction(_Request);

            return _Response;
        }

        [HttpGet]
        public async Task<IResult> GetTransactions()
        {
            var handler = new GetTransactionsHandler(mapper, context);

            var _Response = await handler.GetTransactions();

            return _Response;
        }

        [HttpPost("Update")]
        public async Task<IResult> UpdateTransaction([FromBody] UpdateTransactionRequest request)
        {
            var handler = new UpdateTransactionHandler(mapper, context);

            var _Result = await handler.UpdateTransaction(request);

            return _Result;
        }
    }
}
