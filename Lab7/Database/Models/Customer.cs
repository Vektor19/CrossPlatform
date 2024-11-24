using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab7.Database.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string CustomerAddressLine1 { get; set; }
        public string CustomerAddressLine2 { get; set; }
        public string CustomerAddressLine3 { get; set; }
        public string TownCity { get; set; }
        public string State { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerOtherDetails { get; set; }
        public ICollection<Contract>? Contracts { get; set; }
        public ICollection<CustomerCall>? CustomerCalls { get; set; }
    }
}
