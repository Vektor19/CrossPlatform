using Lab6.Database.Models;

namespace Lab6.Models
{
    public class CustomerModel
    {
        public string CustomerAddressLine1 { get; set; }
        public string CustomerAddressLine2 { get; set; }
        public string CustomerAddressLine3 { get; set; }
        public string TownCity { get; set; }
        public string State { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerOtherDetails { get; set; }
    }
}

