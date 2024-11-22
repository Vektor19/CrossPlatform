using Lab6.Database.Models;

namespace Lab6.RequestModels
{
    public class ContractModel
    {
        public string CustomerId { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string OtherDetails { get; set; }
        public Customer? Customer { get; set; }
    }
}

