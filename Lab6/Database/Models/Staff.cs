using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab6.Database.Models
{
    public class Staff
    {
        public string StaffId { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string OtherDetails { get; set; }
        public ICollection<CustomerCall>? CustomerCalls { get; set; }
    }
}
