using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace assetsmentCelsia.Models
{
    public class Transaction
    {
        public string? Id { get; set; }
        public DateTime? DateTimeTransaction { get; set; }
        public int? Amount { get; set; }
        public string? State { get; set; }
        public string? Type { get; set; }
        public int? PlatformId { get; set; }
        public int? InvoiceId { get; set; }
        public Platform? Platform { get; set; }
        public Invoice? Invoice { get; set; }
    }
}