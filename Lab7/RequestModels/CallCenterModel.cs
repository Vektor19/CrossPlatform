using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab7.RequestModels
{
    public class CallCenterModel
    {
        public string CallCenterId { get; set; }
        public string CallCenterAddress { get; set; }
        public string CallCenterOtherDetails { get; set; }
    }
}
