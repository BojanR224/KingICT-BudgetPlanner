using BudgetPlanner.Models.Dtos;
using BudgetPlanner.Web.Services.Impl;
using System.Net.Http.Json;

namespace BudgetPlanner.Web.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly HttpClient httpClient;

        public IncomeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IncomeDto?> AddIncome(IncomeDto income)
        {
            var response = await httpClient.PostAsJsonAsync("api/Income", income);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IncomeDto>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to add income");
        }

        public async Task<bool> DeleteIncome(int id)
        {
            var response = await httpClient.DeleteAsync($"api/Income/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<IncomeCategoryDto>> GetCategories()
        {
            var response = await httpClient.GetAsync("api/Income/GetCategories");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<IncomeCategoryDto>>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to fetch categories");
        }

        public async Task<IncomeCategoryDto?> GetCategory(string name)
        {
            var response = await httpClient.GetAsync($"api/Income/Category/{name}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IncomeCategoryDto>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to fetch category");
        }

        public async Task<IEnumerable<IncomeDto>> GetIncomes()
        {
            var response = await httpClient.GetAsync("api/Income");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<IncomeDto>>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to fetch incomes");
        }

        public async Task<IncomeDto?> GetItem(int id)
        {
            var response = await httpClient.GetAsync($"api/Income/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IncomeDto>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to fetch income");
        }

        public async Task<IncomeDto?> UpdateIncome(IncomeDto income)
        {
            var response = await httpClient.PutAsJsonAsync($"api/Income/{income.Id}", income);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IncomeDto>();
                if (result == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }
                return result;
            }
            throw new Exception("Failed to update income");
        }

        public async Task<IncomeDto?> AddOrUpdateExpense(IncomeDto income)
        {
            var incomes = await this.GetIncomes();
            bool idExists = incomes.Any(i => i.Id == income.Id);

            if (!idExists)
            {
                return await AddIncome(income);
            }
            else
            {
                return await UpdateIncome(income);
            }
        }
    }
}
