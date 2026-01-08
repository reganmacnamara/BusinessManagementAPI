using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceAutomationAPI.Models
{

    public class Transaction
    {
        [Key]
        public long TransactionID { get; set; }

        public DateTime TransactionDate { get; set; }

        public string TransactionType { get; init; }

        public DateOnly DueDate { get; set; }

        public Client Client { get; set; } = null!;

        [Column(TypeName = "decimal(19, 2)")]
        public decimal GrossValue { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal TaxValue { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal NetValue { get; set; }

        [Column(TypeName = "decimal(19, 2)")]
        public decimal OffsetValue { get; set; }
    }

}
