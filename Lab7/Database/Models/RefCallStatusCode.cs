using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab7.Database.Models
{
    public class RefCallStatusCode
    {
        public string CallStatusCode { get; set; }
        public string CallStatusDescription { get; set; }
        public string CallStatusComments { get; set; }
        public ICollection<CustomerCall>? CustomerCalls { get; set; }
    }
}
