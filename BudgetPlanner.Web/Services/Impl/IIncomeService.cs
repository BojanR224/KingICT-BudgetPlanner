using BudgetPlanner.Models.Dtos;

namespace BudgetPlanner.Web.Services.Impl
{
    public interface IIncomeService
    {
        public Task<IEnumerable<IncomeDto>> GetIncomes();

        public Task<IEnumerable<IncomeCategoryDto>> GetCategories();

        public Task<IncomeDto> GetItem(int id);

        public Task<IncomeCategoryDto> GetCategory(string name);

        public Task<IncomeDto> UpdateIncome(IncomeDto income);

        public Task<IncomeDto> AddIncome(IncomeDto income);

        public Task<bool> DeleteIncome(int id);

        public Task<IncomeDto> AddOrUpdateExpense(IncomeDto income);


    }
}
