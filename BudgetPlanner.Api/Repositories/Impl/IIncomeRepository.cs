using BudgetPlanner.Api.Entities;
using BudgetPlanner.Models.Dtos;

namespace BudgetPlanner.Api.Repositories.Impl
{
    public interface IIncomeRepository
    {
        public Task<IEnumerable<Income>> GetIncomes();

        public Task<IEnumerable<IncomeCategory>> GetCategories();

        public Task<Income> GetIncome(int id);

        public Task<IncomeCategory> GetCategory(int id);

        public Task<IncomeCategory> GetCategory(string name);

        public Task<Income> AddIncome(IncomeDto income);

        public Task<Income> UpdateIncome(IncomeEditDto income);

        public Task<bool> DeleteIncome(int id);


    }
}
