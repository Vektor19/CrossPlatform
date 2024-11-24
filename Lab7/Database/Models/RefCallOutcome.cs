using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab7.Database.Models
{
   public class RefCallOutcome
    {
        public string CallOutcomeCode { get; set; }
        public string CallOutcomeDescription { get; set; }
        public string OtherCallOutcomeDetails { get; set; }
        public ICollection<CustomerCall>? CustomerCalls { get; set; }
    }
}
