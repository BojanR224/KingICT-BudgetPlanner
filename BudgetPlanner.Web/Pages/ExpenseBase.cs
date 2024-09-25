using BudgetPlanner.Models.Dtos;
using BudgetPlanner.Web.Services.Impl;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace BudgetPlanner.Web.Pages
{
    public class ExpenseBase : ComponentBase
    {
        [Inject]
        public IExpenseService ExpenseService { get; set; }

        public IEnumerable<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();

        public IEnumerable<ExpenseCategoryDto> ExpenseCategories { get; set; } = new List<ExpenseCategoryDto>();
        public ExpenseDto NewExpense { get; set; } = new ExpenseDto();
        public int? EditIndex { get; set; }

        public ExpenseBase()
        {
            // Initialize NewExpense with current year and month
            NewExpense = new ExpenseDto
            {
                Year = DateTime.Now.Year,
                Month = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture)
            };
        }
        protected override async Task OnInitializedAsync()
        {

            Expenses = await ExpenseService.GetExpenses();
            ExpenseCategories = await ExpenseService.GetCategories();
        }

        public async Task AddOrUpdateExpense()
        {

            if (EditIndex == null)
            {

                ExpenseDto expense = new ExpenseDto
                {
                    Month = NewExpense.Month,
                    Year = NewExpense.Year,
                    CategoryId = NewExpense.CategoryId,
                    Amount = NewExpense.Amount,
                    IsPlanned = NewExpense.IsPlanned
                };

                await ExpenseService.AddOrUpdateExpense(expense);
                ResetNewExpense();
            }
            else
            {
                var existingExpense = Expenses.FirstOrDefault(e => e.Id == EditIndex);
                if (existingExpense != null)
                {
                    existingExpense.Month = NewExpense.Month;
                    existingExpense.Year = NewExpense.Year;
                    existingExpense.CategoryId = NewExpense.CategoryId;
                    existingExpense.Amount = NewExpense.Amount;
                    existingExpense.IsPlanned = NewExpense.IsPlanned;
                }

                await ExpenseService.AddOrUpdateExpense(NewExpense);
                ResetNewExpense();
                EditIndex = null;
            }

            //    if (EditIndex != null)
            //{
            //    var existingExpense = Expenses.FirstOrDefault(e => e.Id == EditIndex);
            //    if (existingExpense != null)
            //    {
            //        // Update expense properties
            //        existingExpense.Month = NewExpense.Month;
            //        existingExpense.Year = NewExpense.Year;
            //        existingExpense.CategoryName = NewExpense.CategoryName;
            //        existingExpense.Amount = NewExpense.Amount;
            //        existingExpense.IsPlanned = NewExpense.IsPlanned;
            //    }
            //}

            await ExpenseService.AddOrUpdateExpense(NewExpense);
            ResetNewExpense();
            EditIndex = null;

            // Refresh list
            Expenses = await ExpenseService.GetExpenses();
        }

        public void EditExpense(ExpenseDto expense)
        {
            NewExpense = new ExpenseDto
            {
                Id = expense.Id,
                Month = expense.Month,
                Year = expense.Year,
                CategoryName = expense.CategoryName,
                Amount = expense.Amount,
                IsPlanned = expense.IsPlanned
            };
            EditIndex = expense.Id;
        }

        public async Task DeleteExpense(int id)
        {
            await ExpenseService.DeleteExpense(id);
            Expenses = await ExpenseService.GetExpenses();
        }

        public void CancelEdit()
        {
            ResetNewExpense();
            EditIndex = null;
        }

        private void ResetNewExpense()
        {
            NewExpense = new ExpenseDto
            {
                Year = DateTime.Now.Year,
                Month = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture)
            };
        }

    }
}