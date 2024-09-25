using BudgetPlanner.Models.Dtos;

namespace BudgetPlanner.Web.Services.Impl
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDto>> GetExpenses();
        Task<ExpenseDto> GetItem(int id);

        Task<IEnumerable<ExpenseCategoryDto>> GetCategories();

        Task<ExpenseDto> AddOrUpdateExpense(ExpenseDto expense);

        Task DeleteExpense(int id);

        Task<string> GetCategory(int id);

    }
}
