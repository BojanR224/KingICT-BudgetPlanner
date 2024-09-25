using BudgetPlanner.Api.Entities;
using BudgetPlanner.Models.Dtos;

namespace BudgetPlanner.Api.Repositories.Impl
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetExpenses();

        Task<IEnumerable<ExpenseCategory>> GetCategories();

        Task<Expense> GetExpense(int id);

        Task<ExpenseCategory> GetCategory(int id);

        Task<Expense> AddExpense(ExpenseDto expense);

        Task<Expense> UpdateExpense(ExpenseDto expense);

        Task<bool> DeleteExpense(int id);
    }
}
