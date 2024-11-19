using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab6.Database.Models
{
    public class CustomerCall
    {
        public string CallId { get; set; }
        public string CustomerId { get; set; }
        public string CallCenterId { get; set; }
        public string CallOutcomeCode { get; set; }
        public string CallStatusCode { get; set; }
        public string RecommendedSolutionId { get; set; }
        public string StaffId { get; set; }
        public DateTime CallDateTime { get; set; }
        public string CallDescription { get; set; }
        public string TailoredSolutionDescription { get; set; }
        public string CallOtherDetails { get; set; }
        public Customer Customer { get; set; }
        public CallCenter CallCenter { get; set; }
        public RefCallOutcome CallOutcome { get; set; }
        public RefCallStatusCode CallStatus { get; set; }
        public CommonSolution RecommendedSolution { get; set; }
        public Staff Staff { get; set; }
    }
}
