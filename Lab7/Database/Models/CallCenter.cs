using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab6.Database.Models
{
    public class CallCenter
    {
        public string CallCenterId { get; set; }
        public string CallCenterAddress { get; set; }
        public string CallCenterOtherDetails { get; set; }
        public ICollection<CustomerCall>? CustomerCalls { get; set; }
    }
}
