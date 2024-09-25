using BudgetPlanner.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Api.Data
{
    public class BudgetPlannerDbContext : DbContext
    {
        public BudgetPlannerDbContext(DbContextOptions<BudgetPlannerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Income>().
                HasKey(i => i.Id);

            modelBuilder.Entity<Income>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Income>()
            .HasOne(i => i.Category)
            .WithMany() 
            .HasForeignKey(i => i.CategoryId);

            modelBuilder.Entity<Expense>().
                HasKey(i => i.Id);

            modelBuilder.Entity<Expense>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.ExpenseCategory)
                .WithMany()
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<ExpenseCategory>().HasData(
               new ExpenseCategory { Id = 1, Name = "Храна" },
               new ExpenseCategory { Id = 2, Name = "Превоз" },
               new ExpenseCategory { Id = 3, Name = "Сметки" },
               new ExpenseCategory { Id = 4, Name = "Останато" }
           );

            modelBuilder.Entity<IncomeCategory>().HasData(
               new IncomeCategory { Id = 1, Name = "Плата" },
               new IncomeCategory { Id = 2, Name = "Патни Трошоци" },
               new IncomeCategory { Id = 3, Name = "Останато" }
           );
            modelBuilder.Entity<Expense>().HasData(
                new Expense { Id = 1, Month = "January", Year = 2024, CategoryId = 1, Amount = 4.55F, IsPlanned = true },
                new Expense { Id = 2, Month = "February", Year = 2024, CategoryId = 2, Amount = 4.55F, IsPlanned = true },
                new Expense { Id = 3, Month = "March", Year = 2024, CategoryId = 1, Amount = 61.5F, IsPlanned = false },
                new Expense { Id = 4, Month = "April", Year = 2024, CategoryId = 2, Amount = 6.00F, IsPlanned = true }
            );

            modelBuilder.Entity<Income>().HasData(
                new Income { Id = 1, Month = "January", Year = 2024, CategoryId = 1, Amount = 4.55F, IsPlanned = true },
                new Income { Id = 2, Month = "February", Year = 2024, CategoryId = 2, Amount = 4.55F, IsPlanned = true },
                new Income { Id = 3, Month = "March", Year = 2024, CategoryId = 1, Amount = 61.5F, IsPlanned = false },
                new Income { Id = 4, Month = "April", Year = 2024, CategoryId = 3, Amount = 6.00F, IsPlanned = true }
            );
        }
        public DbSet<Expense> Expenses { get; set; }

        public DbSet<ExpenseCategory> ExpenseCategory { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public DbSet<IncomeCategory> IncomeCategory { get; set; }
    }
}
