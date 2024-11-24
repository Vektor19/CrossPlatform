using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab7.Database.Models
{
    public class SolutionsForCommonProblems
    {
        public string ProblemId { get; set; }
        public string SolutionId { get; set; }
        public CommonProblem CommonProblem { get; set; }
        public CommonSolution CommonSolution { get; set; }
    }
}
