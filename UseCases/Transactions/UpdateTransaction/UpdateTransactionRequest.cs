namespace BusinessManagementAPI.UseCases.Transactions.UpdateTransaction;

public class UpdateTransactionRequest
{
    public long TransactionId { get; set; }

    public DateTime DueDate { get; set; }

}
