using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab6.Database.Models
{
    public class Contract
    {
        public string ContractId { get; set; }
        public string CustomerId { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string OtherDetails { get; set; }
        public Customer? Customer { get; set; }
    }
}
