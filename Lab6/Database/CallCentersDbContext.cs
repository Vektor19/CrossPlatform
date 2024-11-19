using Microsoft.EntityFrameworkCore;
using Lab6.Database.Models;
namespace Lab6.Database
{
    public class CallCentersDbContext: DbContext
    {
        public CallCentersDbContext(DbContextOptions<CallCentersDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CallCenter> CallCenters { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<CommonProblem> CommonProblems { get; set; }
        public DbSet<CommonSolution> CommonSolutions { get; set; }
        public DbSet<RefCallOutcome> RefCallOutcomes { get; set; }
        public DbSet<RefCallStatusCode> RefCallStatusCodes { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<CustomerCall> CustomerCalls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(b => {
                b.HasMany(cu => cu.Contracts)
                 .WithOne(co => co.Customer)
                 .HasForeignKey(co => co.CustomerId)
                 .HasPrincipalKey(cu => cu.CustomerId);
                b.HasMany(cu => cu.CustomerCalls)
                 .WithOne(cc => cc.Customer)
                 .HasForeignKey(cc => cc.CustomerId)
                 .HasPrincipalKey(cu => cu.CustomerId);
            });

            modelBuilder.Entity<CallCenter>(b => {
                b.HasMany(cc => cc.CustomerCalls)
                 .WithOne(cc => cc.CallCenter)
                 .HasForeignKey(cc => cc.CallCenterId)
                 .HasPrincipalKey(cc => cc.CallCenterId);
            });
            modelBuilder.Entity<RefCallOutcome>(b => {
                b.HasMany(rco => rco.CustomerCalls)
                 .WithOne(cc => cc.CallOutcome)
                 .HasForeignKey(cc => cc.CallOutcomeCode)
                 .HasPrincipalKey(rco => rco.CallOutcomeCode);
            });
            modelBuilder.Entity<RefCallStatusCode>(b => {
                b.HasMany(rcs => rcs.CustomerCalls)
                 .WithOne(cc => cc.CallStatus)
                 .HasForeignKey(cc => cc.CallStatusCode)
                 .HasPrincipalKey(rcs => rcs.CallStatusCode);
            });
            modelBuilder.Entity<CommonSolution>(b => {
                b.HasMany(cs => cs.CustomerCalls)
                 .WithOne(cc => cc.RecommendedSolution)
                 .HasForeignKey(cc => cc.RecommendedSolutionId)
                 .HasPrincipalKey(cs => cs.SolutionId);
                b.HasMany(cs => cs.CommonProblems)
                 .WithMany(cp => cp.CommonSolutions)
                 .UsingEntity<SolutionsForCommonProblems>(
                     problem => problem
                        .HasOne(sfp => sfp.CommonProblem)
                        .WithMany(cp => cp.SolutionsForCommonProblems)
                        .HasForeignKey(sfp => sfp.ProblemId),
                     solution => solution
                        .HasOne(sfp => sfp.CommonSolution)
                        .WithMany(cs => cs.SolutionsForCommonProblems)
                        .HasForeignKey(sfp => sfp.SolutionId)
                 );
            });
            modelBuilder.Entity<Staff>(b => {
                b.HasMany(s => s.CustomerCalls)
                 .WithOne(cc => cc.Staff)
                 .HasForeignKey(cc => cc.StaffId)
                 .HasPrincipalKey(s => s.StaffId);
            });
        }
    }
}
