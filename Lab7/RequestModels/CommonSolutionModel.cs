using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Lab7.RequestModels
{
    public class CommonSolutionModel
    {
        public string SolutionDescription { get; set; }
        public string OtherSolutionDetails { get; set; }
    }
}
