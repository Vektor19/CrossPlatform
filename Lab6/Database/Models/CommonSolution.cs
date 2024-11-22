using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Lab6.Database.Models
{
    public class CommonSolution
    {
        public string SolutionId { get; set; }
        public string SolutionDescription { get; set; }
        public string OtherSolutionDetails { get; set; }
        public ICollection<CustomerCall>? CustomerCalls { get; set; }
        public ICollection<CommonProblem>? CommonProblems { get; set; }
        [JsonIgnore]
        public List<SolutionsForCommonProblems> SolutionsForCommonProblems { get; set; } = new List<SolutionsForCommonProblems>();

    }
}
