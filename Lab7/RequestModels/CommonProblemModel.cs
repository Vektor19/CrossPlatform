using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Lab7.RequestModels
{
    public class CommonProblemModel
    {
        public string ProblemDescription { get; set; }
        public string OtherProblemDetails { get; set; }
    }
}
