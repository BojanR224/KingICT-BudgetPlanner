using BudgetPlanner.Api.Data;
using BudgetPlanner.Api.Entities;
using BudgetPlanner.Api.Repositories.Impl;
using BudgetPlanner.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Api.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {

        private readonly BudgetPlannerDbContext budgetPlannerDbContext;
        public IncomeRepository(BudgetPlannerDbContext budgetPlannerDbContext)
        {
            this.budgetPlannerDbContext = budgetPlannerDbContext;
        }

        public async Task<Income> AddIncome(IncomeDto income)
        {
            var incomeCategory = await this.budgetPlannerDbContext.IncomeCategory.FindAsync(income.CategoryId);
            if (incomeCategory == null)
            {
                throw new Exception($"Category with ID {income.CategoryId} does not exist");
            }

            
            var incomeNew = new Income
            {
                Month = income.Month,
                Year = income.Year,
                CategoryId = income.CategoryId,
                Amount = income.Amount,
                IsPlanned = income.IsPlanned,
                Category = incomeCategory
            };

            var result = await this.budgetPlannerDbContext.Incomes.AddAsync(incomeNew);
            await this.budgetPlannerDbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<bool> DeleteIncome(int id)
        {
            var income = await this.budgetPlannerDbContext.Incomes.FindAsync(id);

            if (income != null)
            {
                this.budgetPlannerDbContext.Incomes.Remove(income);

                await this.budgetPlannerDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<IncomeCategory>> GetCategories()
        {
            var categories = await this.budgetPlannerDbContext.IncomeCategory.ToListAsync();

            return categories;
        }

        public async Task<IncomeCategory> GetCategory(int id)
        {
            var category = await this.budgetPlannerDbContext.IncomeCategory.FindAsync(id);

            if (category != null)
            {
                return category;
            }

            return null;
        }

        public async Task<IncomeCategory> GetCategory(string name)
        {
            var category = await this.budgetPlannerDbContext.IncomeCategory.SingleOrDefaultAsync(e => e.Name == name);

            if (category != null)
            {
                return category;
            }

            return null;
        }

        public async Task<Income> GetIncome(int id)
        {
            var income = await this.budgetPlannerDbContext.Incomes.FindAsync(id);

            if (income != null)
            {
                return income;
            }

            return null;
        }

        public async Task<IEnumerable<Income>> GetIncomes()
        {
            var incomes = await this.budgetPlannerDbContext.Incomes.Include(e => e.Category).ToListAsync();

            return incomes;
        }

        public async Task<Income> UpdateIncome(IncomeEditDto income)
        {
            var incomeToUpdate = await this.budgetPlannerDbContext.Incomes.FindAsync(income.Id);

            if (incomeToUpdate != null)
            {
                incomeToUpdate.Month = income.Month;
                incomeToUpdate.Year = income.Year;
                incomeToUpdate.Amount = income.Amount;

                await this.budgetPlannerDbContext.SaveChangesAsync();

                return incomeToUpdate;
            }

            return null;
        }
    }
}
