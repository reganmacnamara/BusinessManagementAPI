using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Invoices.CreateInvoice;
using BusinessManagementAPI.UseCases.Invoices.DeleteInvoice;
using BusinessManagementAPI.UseCases.Invoices.DeleteInvoiceItem;
using BusinessManagementAPI.UseCases.Invoices.GetClientInvoices;
using BusinessManagementAPI.UseCases.Invoices.GetInvoice;
using BusinessManagementAPI.UseCases.Invoices.GetInvoices;
using BusinessManagementAPI.UseCases.Invoices.UpdateInvoice;
using BusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceController(IMapper mapper, SQLContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateInvoice([FromBody] CreateInvoiceRequest request)
    {
        var handler = new CreateInvoiceHandler(mapper, context);

        var _Response = await handler.CreateInvoice(request);

        return _Response;
    }

    [HttpDelete("{invoiceID}")]
    public async Task<IResult> DeleteInvoice([FromRoute] long invoiceID)
    {
        var _Request = new DeleteInvoiceRequest()
        {
            InvoiceID = invoiceID
        };

        var handler = new DeleteInvoiceHandler(mapper, context);

        var _Response = await handler.DeleteInvoice(_Request);

        return _Response;
    }

    [HttpDelete("Item/{invoiceItemID}")]
    public async Task<IResult> DeleteInvoiceItem([FromRoute] long invoiceItemID)
    {
        var _Request = new DeleteInvoiceItemRequest()
        {
            InvoiceItemID = invoiceItemID
        };

        var handler = new DeleteInvoiceItemHandler(mapper, context);

        var _Response = await handler.DeleteInvoiceItem(_Request);

        return _Response;
    }

    [HttpGet("Client/{clientID}")]
    public async Task<IResult> GetClientInvoices([FromRoute] long clientID)
    {
        var _Request = new GetClientInvoicesRequest()
        {
            ClientID = clientID
        };

        var handler = new GetClientInvoicesHandler(mapper, context);

        var _Response = await handler.GetClientInvoices(_Request);

        return _Response;
    }

    [HttpGet("{invoiceID}")]
    public async Task<IResult> GetInvoice([FromRoute] long invoiceID)
    {
        var _Request = new GetInvoiceRequest()
        {
            InvoiceID = invoiceID
        };

        var handler = new GetInvoiceHandler(mapper, context);

        var _Response = await handler.GetInvoice(_Request);

        return _Response;
    }

    [HttpGet]
    public async Task<IResult> GetInvoices()
    {
        var handler = new GetInvoicesHandler(mapper, context);

        var _Response = await handler.GetInvoices();

        return _Response;
    }

    [HttpPatch]
    public async Task<IResult> UpdateInvoice([FromBody] UpdateInvoiceRequest request)
    {
        var handler = new UpdateInvoiceHandler(mapper, context);

        var _Response = await handler.UpdateInvoice(request);

        return _Response;
    }

    [HttpPut("Item")]
    public async Task<IResult> UpsertInvoiceItem([FromBody] UpsertInvoiceItemRequest request)
    {
        var handler = new UpsertInvoiceItemHandler(mapper, context);

        var _Response = request.InvoiceItemID != 0
            ? await handler.UpdateInvoiceItem(request)
            : await handler.CreateInvoiceItem(request);

        return _Response;
    }
}
