using BudgetPlanner.Models.Dtos;
using BudgetPlanner.Web.Services.Impl;
using System.Net.Http.Json;

namespace BudgetPlanner.Web.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly HttpClient httpClient;

        public ExpenseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ExpenseDto>> GetExpenses()
        {
            var response = await httpClient.GetAsync("api/Expense");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<ExpenseDto>>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to fetch expenses");
        }

        public async Task<ExpenseDto?> GetExpense(int id)
        {
            var response = await httpClient.GetAsync($"api/Expense/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ExpenseDto>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to fetch expense");
        }

        public async Task<ExpenseDto?> AddOrUpdateExpense(ExpenseDto expense)
        {
            var expenses = await this.GetExpenses();
            bool idExists = expenses.Any(e => e.Id == expense.Id);

            if (!idExists)
            {
                return await AddExpense(expense);
            }
            else
            {
                return await UpdateExpense(expense);
            }
        }

        public async Task<ExpenseDto?> AddExpense(ExpenseDto expense)
        {
            var response = await httpClient.PostAsJsonAsync("api/Expense", expense);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ExpenseDto>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to add expense");
        }

        public async Task<ExpenseDto?> UpdateExpense(ExpenseDto expense)
        {
            var response = await httpClient.PutAsJsonAsync($"api/Expense/{expense.Id}", expense);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ExpenseDto>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to update expense");
        }

        public async Task DeleteExpense(int id)
        {
            var response = await httpClient.DeleteAsync($"api/Expense/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete expense");
            }
        }

        public async Task<ExpenseDto?> GetItem(int id)
        {
            var response = await httpClient.GetAsync($"api/Expense/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ExpenseDto>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to fetch expense");
        }

        public async Task<IEnumerable<ExpenseCategoryDto>> GetCategories()
        {
            var response = await httpClient.GetAsync("api/Expense/GetCategories");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<ExpenseCategoryDto>>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to fetch categories");
        }

        public async Task<string> GetCategory(int id)
        {
            var categories = await this.GetCategories();
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category.Name;
        }
    }
}
