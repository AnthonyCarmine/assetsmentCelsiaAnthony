using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace assetsmentCelsia.Models
{
    public class Invoice
    {
        public int? Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? PeriodInvoicing { get; set; }
        public int? InvoicedAmount { get; set; }
        public int? AmountPaid { get; set; }
        public int? UserId { get; set; }
       public User? User { get; set; }

        [JsonIgnore]
        public List<Transaction>? Transactions { get; set; }
    }
}