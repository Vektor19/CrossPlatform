using Microsoft.EntityFrameworkCore;
using Lab6.Database.Models;
using Microsoft.SqlServer.Server;
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
            modelBuilder.Entity<Customer>(b =>
            {
                b.HasMany(cu => cu.Contracts)
                 .WithOne(co => co.Customer)
                 .HasForeignKey(co => co.CustomerId)
                 .HasPrincipalKey(cu => cu.CustomerId);
                b.HasMany(cu => cu.CustomerCalls)
                 .WithOne(cc => cc.Customer)
                 .HasForeignKey(cc => cc.CustomerId)
                 .HasPrincipalKey(cu => cu.CustomerId);
            });

            modelBuilder.Entity<CallCenter>(b =>
            {
                b.HasMany(cc => cc.CustomerCalls)
                 .WithOne(cc => cc.CallCenter)
                 .HasForeignKey(cc => cc.CallCenterId)
                 .HasPrincipalKey(cc => cc.CallCenterId);
            });
            modelBuilder.Entity<RefCallOutcome>(b =>
            {   
                b.HasKey(rco => rco.CallOutcomeCode);
                b.HasMany(rco => rco.CustomerCalls)
                 .WithOne(cc => cc.CallOutcome)
                 .HasForeignKey(cc => cc.CallOutcomeCode)
                 .HasPrincipalKey(rco => rco.CallOutcomeCode);
            });
            modelBuilder.Entity<RefCallStatusCode>(b =>
            {
                b.HasKey(rcs => rcs.CallStatusCode);    
                b.HasMany(rcs => rcs.CustomerCalls)
                 .WithOne(cc => cc.CallStatus)
                 .HasForeignKey(cc => cc.CallStatusCode)
                 .HasPrincipalKey(rcs => rcs.CallStatusCode);
            });
            modelBuilder.Entity<CommonProblem>(b => b.HasKey(cp => cp.ProblemId));  
            modelBuilder.Entity<CommonSolution>(b =>
            {
                b.HasKey(cs => cs.SolutionId);
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
            modelBuilder.Entity<Staff>(b =>
            {
                b.HasMany(s => s.CustomerCalls)
                 .WithOne(cc => cc.Staff)
                 .HasForeignKey(cc => cc.StaffId)
                 .HasPrincipalKey(s => s.StaffId);
            });
            modelBuilder.Entity<CustomerCall>(b => b.HasKey(cc => cc.CallId));

            var callCenterIds = new string[4];
            var commonProblemIds = new string[4];
            var staffIds = new string[4];
            var callOutcomeCodes = new string[4];
            var callStatusCodes = new string[4];
            var solutionIds = new string[4];
            var customerIds = new string[4];
            var contractIds = new string[4];

            for (int i = 0; i < 4; i++)
            {
                callCenterIds[i] = Guid.NewGuid().ToString();
                commonProblemIds[i] = Guid.NewGuid().ToString();
                staffIds[i] = Guid.NewGuid().ToString();
                callOutcomeCodes[i] = Guid.NewGuid().ToString();
                callStatusCodes[i] = Guid.NewGuid().ToString();
                solutionIds[i] = Guid.NewGuid().ToString();
                customerIds[i] = Guid.NewGuid().ToString();
                contractIds[i] = Guid.NewGuid().ToString();
            }

            modelBuilder.Entity<CallCenter>().HasData(
                new CallCenter { CallCenterId = callCenterIds[0], CallCenterAddress = "Address 1", CallCenterOtherDetails = "Details 1" },
                new CallCenter { CallCenterId = callCenterIds[1], CallCenterAddress = "Address 2", CallCenterOtherDetails = "Details 2" },
                new CallCenter { CallCenterId = callCenterIds[2], CallCenterAddress = "Address 3", CallCenterOtherDetails = "Details 3" },
                new CallCenter { CallCenterId = callCenterIds[3], CallCenterAddress = "Address 4", CallCenterOtherDetails = "Details 4" }
            );

            modelBuilder.Entity<CommonProblem>().HasData(
                new CommonProblem { ProblemId = commonProblemIds[0], ProblemDescription = "Problem 1", OtherProblemDetails = "Details 1" },
                new CommonProblem { ProblemId = commonProblemIds[1], ProblemDescription = "Problem 2", OtherProblemDetails = "Details 2" },
                new CommonProblem { ProblemId = commonProblemIds[2], ProblemDescription = "Problem 3", OtherProblemDetails = "Details 3" },
                new CommonProblem { ProblemId = commonProblemIds[3], ProblemDescription = "Problem 4", OtherProblemDetails = "Details 4" }
            );

            modelBuilder.Entity<Staff>().HasData(
                new Staff { StaffId = staffIds[0], PhoneNumber = "+380504567890", EmailAddress = "example1@gmail.com", OtherDetails = "Details 1" },
                new Staff { StaffId = staffIds[1], PhoneNumber = "+380504567891", EmailAddress = "example2@gmail.com", OtherDetails = "Details 2" },
                new Staff { StaffId = staffIds[2], PhoneNumber = "+380504567892", EmailAddress = "example3@gmail.com", OtherDetails = "Details 3" },
                new Staff { StaffId = staffIds[3], PhoneNumber = "+380504567893", EmailAddress = "example4@gmail.com", OtherDetails = "Details 4" }
            );

            modelBuilder.Entity<RefCallOutcome>().HasData(
                new RefCallOutcome { CallOutcomeCode = callOutcomeCodes[0], CallOutcomeDescription = "Outcome 1", OtherCallOutcomeDetails = "Details 1" },
                new RefCallOutcome { CallOutcomeCode = callOutcomeCodes[1], CallOutcomeDescription = "Outcome 2", OtherCallOutcomeDetails = "Details 2" },
                new RefCallOutcome { CallOutcomeCode = callOutcomeCodes[2], CallOutcomeDescription = "Outcome 3", OtherCallOutcomeDetails = "Details 3" },
                new RefCallOutcome { CallOutcomeCode = callOutcomeCodes[3], CallOutcomeDescription = "Outcome 4", OtherCallOutcomeDetails = "Details 4" }
            );

            modelBuilder.Entity<RefCallStatusCode>().HasData(
                new RefCallStatusCode { CallStatusCode = callStatusCodes[0], CallStatusDescription = "Status 1", CallStatusComments = "Comments 1" },
                new RefCallStatusCode { CallStatusCode = callStatusCodes[1], CallStatusDescription = "Status 2", CallStatusComments = "Comments 2" },
                new RefCallStatusCode { CallStatusCode = callStatusCodes[2], CallStatusDescription = "Status 3", CallStatusComments = "Comments 3" },
                new RefCallStatusCode { CallStatusCode = callStatusCodes[3], CallStatusDescription = "Status 4", CallStatusComments = "Comments 4" }
            );

            modelBuilder.Entity<CommonSolution>().HasData(
                new CommonSolution { SolutionId = solutionIds[0], SolutionDescription = "Solution 1", OtherSolutionDetails = "Details 1" },
                new CommonSolution { SolutionId = solutionIds[1], SolutionDescription = "Solution 2", OtherSolutionDetails = "Details 2" },
                new CommonSolution { SolutionId = solutionIds[2], SolutionDescription = "Solution 3", OtherSolutionDetails = "Details 3" },
                new CommonSolution { SolutionId = solutionIds[3], SolutionDescription = "Solution 4", OtherSolutionDetails = "Details 4" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = customerIds[0], CustomerAddressLine1 = "Address 1", CustomerAddressLine2 = "Address 2", CustomerAddressLine3 = "Address 3", EmailAddress = "emailAddress1@gmail.com", PhoneNumber = "+380904567890", TownCity = "City 1", State = "State 1", CustomerOtherDetails = "Details 1" },
                new Customer { CustomerId = customerIds[1], CustomerAddressLine1 = "Address 2", CustomerAddressLine2 = "Address 3", CustomerAddressLine3 = "Address 4", EmailAddress = "emailAddress2@gmail.com", PhoneNumber = "+380904567891", TownCity = "City 2", State = "State 2", CustomerOtherDetails = "Details 2" },
                new Customer { CustomerId = customerIds[2], CustomerAddressLine1 = "Address 3", CustomerAddressLine2 = "Address 4", CustomerAddressLine3 = "Address 5", EmailAddress = "emailAddress3@gmail.com", PhoneNumber = "+380904567892", TownCity = "City 3", State = "State 3", CustomerOtherDetails = "Details 3" },
                new Customer { CustomerId = customerIds[3], CustomerAddressLine1 = "Address 4", CustomerAddressLine2 = "Address 5", CustomerAddressLine3 = "Address 6", EmailAddress = "emailAddress4@gmail.com", PhoneNumber = "+380904567893", TownCity = "City 4", State = "State 4", CustomerOtherDetails = "Details 4" }
            );

            modelBuilder.Entity<Contract>().HasData(
                new Contract { ContractId = contractIds[0], CustomerId = customerIds[0], ContractStartDate = DateTime.Now, ContractEndDate = DateTime.Now.AddYears(1), OtherDetails = "Details 1" },
                new Contract { ContractId = contractIds[1], CustomerId = customerIds[1], ContractStartDate = DateTime.Now, ContractEndDate = DateTime.Now.AddYears(1), OtherDetails = "Details 2" },
                new Contract { ContractId = contractIds[2], CustomerId = customerIds[2], ContractStartDate = DateTime.Now, ContractEndDate = DateTime.Now.AddYears(1), OtherDetails = "Details 3" },
                new Contract { ContractId = contractIds[3], CustomerId = customerIds[3], ContractStartDate = DateTime.Now, ContractEndDate = DateTime.Now.AddYears(1), OtherDetails = "Details 4" }
            );

            modelBuilder.Entity<CustomerCall>().HasData(
                new CustomerCall
                {
                    CallId = Guid.NewGuid().ToString(),
                    CustomerId = customerIds[0],
                    CallCenterId = callCenterIds[0],
                    CallOutcomeCode = callOutcomeCodes[0],
                    CallStatusCode = callStatusCodes[0],
                    RecommendedSolutionId = solutionIds[0],
                    StaffId = staffIds[0],
                    CallDateTime = DateTime.Now,
                    TailoredSolutionDescription = "Tailored Solution 1",
                    CallDescription = "Description 1",
                    CallOtherDetails = "Details 1"
                },
                new CustomerCall
                {
                    CallId = Guid.NewGuid().ToString(),
                    CustomerId = customerIds[1],
                    CallCenterId = callCenterIds[1],
                    CallOutcomeCode = callOutcomeCodes[1],
                    CallStatusCode = callStatusCodes[1],
                    RecommendedSolutionId = solutionIds[1],
                    StaffId = staffIds[1],
                    CallDateTime = DateTime.Now,
                    TailoredSolutionDescription = "Tailored Solution 2",
                    CallDescription = "Description 2",
                    CallOtherDetails = "Details 2"
                },
                new CustomerCall
                {
                    CallId = Guid.NewGuid().ToString(),
                    CustomerId = customerIds[2],
                    CallCenterId = callCenterIds[2],
                    CallOutcomeCode = callOutcomeCodes[2],
                    CallStatusCode = callStatusCodes[2],
                    RecommendedSolutionId = solutionIds[2],
                    StaffId = staffIds[2],
                    CallDateTime = DateTime.Now,
                    TailoredSolutionDescription = "Tailored Solution 3",
                    CallDescription = "Description 3",
                    CallOtherDetails = "Details 3"
                },
                new CustomerCall
                {
                    CallId = Guid.NewGuid().ToString(),
                    CustomerId = customerIds[3],
                    CallCenterId = callCenterIds[3],
                    CallOutcomeCode = callOutcomeCodes[3],
                    CallStatusCode = callStatusCodes[3],
                    RecommendedSolutionId = solutionIds[3],
                    StaffId = staffIds[3],
                    CallDateTime = DateTime.Now,
                    TailoredSolutionDescription = "Tailored Solution 4",
                    CallDescription = "Description 4",
                    CallOtherDetails = "Details 4"
                }
            );
        }
    }
}
