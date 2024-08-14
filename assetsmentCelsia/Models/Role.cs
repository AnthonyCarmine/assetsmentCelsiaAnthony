using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace assetsmentCelsia.Models
{
    public class Role
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        
        [JsonIgnore]
        public List<Invoice>? Invoices { get; set; }
    }
}