using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab7.Database.Models
{
    public class CustomerCallModel
    {
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
    }
}
