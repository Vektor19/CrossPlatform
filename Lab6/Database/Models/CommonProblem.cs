using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Lab6.Database.Models
{
    public class CommonProblem
    {
        public string ProblemId { get; set; }
        public string ProblemDescription { get; set; }
        public string OtherProblemDetails { get; set; }
        public ICollection<CommonSolution> CommonSolutions { get; set; }
        [JsonIgnore]
        public List<SolutionsForCommonProblems> SolutionsForCommonProblems { get; set; } = new List<SolutionsForCommonProblems>();
    }
}
