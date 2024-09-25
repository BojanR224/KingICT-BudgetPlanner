using BudgetPlanner.Api.Entities;
using BudgetPlanner.Models.Dtos;

namespace BudgetPlanner.Api.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ExpenseDto> ConvertToDto(this IEnumerable<Expense> expenses, IEnumerable<ExpenseCategory> expenseCategories)
        {
            return (from expense in expenses
                    join expenseCategory in expenseCategories on expense.CategoryId equals expenseCategory.Id
                    select new ExpenseDto
                    {
                        Id = expense.Id,
                        Month = expense.Month,
                        Year = expense.Year,
                        CategoryId = expense.CategoryId,
                        Amount = expense.Amount,
                        IsPlanned = expense.IsPlanned,
                        CategoryName = expenseCategory.Name
                    }).ToList();
        }

        public static ExpenseDto ConvertToDto(this Expense expense)
        {
            return  new ExpenseDto
                    {
                        Id = expense.Id,
                        Month = expense.Month,
                        Year = expense.Year,
                        CategoryId = expense.CategoryId,
                        Amount = expense.Amount,
                        IsPlanned = expense.IsPlanned,
                        CategoryName = expense.ExpenseCategory.Name
                    };
        }

        public static IEnumerable<IncomeDto> ConvertToDto(this IEnumerable<Income> incomes, IEnumerable<IncomeCategory> incomeCategories)
        {
            return (from income in incomes
                    join incomeCategory in incomeCategories on income.CategoryId equals incomeCategory.Id
                    select new IncomeDto
                    {
                        Id = income.Id,
                        Month = income.Month,
                        Year = income.Year,
                        CategoryId = income.CategoryId,
                        Amount = income.Amount,
                        IsPlanned = income.IsPlanned,
                        CategoryName = incomeCategory.Name
                    }).ToList();
        }

        public static IncomeDto ConvertToDto(this Income income)
        {
            return new IncomeDto
            {
                Id = income.Id,
                Month = income.Month,
                Year = income.Year,
                CategoryId = income.CategoryId,
                Amount = income.Amount,
                IsPlanned = income.IsPlanned,
                CategoryName = income.Category.Name
            };
        }

        public static IncomeEditDto ConvertToDto(this IncomeDto income)
        {
            return new IncomeEditDto
            {
                Id = income.Id,
                Month = income.Month,
                Year = income.Year,
                Amount = income.Amount
            };
        }
    }
}
