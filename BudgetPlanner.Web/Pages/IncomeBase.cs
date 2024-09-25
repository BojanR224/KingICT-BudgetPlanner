using BudgetPlanner.Models.Dtos;
using BudgetPlanner.Web.Services;
using BudgetPlanner.Web.Services.Impl;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace BudgetPlanner.Web.Pages
{
    public class IncomeBase : ComponentBase
    {
        [Inject]
        public IIncomeService IncomeService { get; set; }

        public IEnumerable<IncomeDto> Incomes { get; set; } = new List<IncomeDto>();

        public IEnumerable<IncomeCategoryDto> IncomeCategories { get; set; } = new List<IncomeCategoryDto>();

        public IncomeDto NewIncome { get; set; } = new IncomeDto();

        public int? EditIndex { get; set; }

        public IncomeBase()
        {
            // Initialize NewExpense with current year and month
            ResetNewIncome();
        }
        protected override async Task OnInitializedAsync()
        {
            Incomes = await IncomeService.GetIncomes();
            IncomeCategories = await IncomeService.GetCategories();
        }

        public async void EditIncome(IncomeDto income)
        {
            var getIncome = await this.IncomeService.GetItem(income.Id);

            NewIncome = new IncomeDto
            {
                Id = income.Id,
                Amount = income.Amount,
                CategoryName = getIncome.CategoryName,
                Month = income.Month,
                Year = income.Year,
                CategoryId = getIncome.CategoryId,
                IsPlanned = getIncome.IsPlanned
            };
            EditIndex = income.Id;

        }

        public async void DeleteIncome(int id)
        {
            await this.IncomeService.DeleteIncome(id);
            Incomes = await this.IncomeService.GetIncomes();
        }

        public void CancelEdit()
        {
            ResetNewIncome();
            EditIndex = null;
        }

        private void ResetNewIncome()
        {
            NewIncome = new IncomeDto
            {
                Year = DateTime.Now.Year,
                Month = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture)
            };
        }

        public async Task AddOrUpdateIncome()
        {
            if (NewIncome.CategoryId == 0)
            {
                // Ensure CategoryId is valid before sending the request
                return;
            }

            if (EditIndex != null)
            {
                var existingIncome = Incomes.FirstOrDefault(e => e.Id == EditIndex);

                if (existingIncome != null)
                {
                    existingIncome.Month = NewIncome.Month;
                    existingIncome.Year = NewIncome.Year;
                    existingIncome.CategoryName = NewIncome.CategoryName;
                    existingIncome.Amount = NewIncome.Amount;
                    existingIncome.IsPlanned = NewIncome.IsPlanned;
                    existingIncome.CategoryId = IncomeService.GetCategory(NewIncome.CategoryName).Id;
                }
            }

            try
            {
                await IncomeService.AddOrUpdateExpense(NewIncome);
                ResetNewIncome();
                EditIndex = null;

                // Refresh the list after adding/updating
                Incomes = await IncomeService.GetIncomes();
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the request
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
