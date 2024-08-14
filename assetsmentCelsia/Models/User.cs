using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace assetsmentCelsia.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string? Names { get; set; }
        public int? Document { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        [JsonIgnore]
        public List<Invoice>? Invoices { get; set; }
    }
}