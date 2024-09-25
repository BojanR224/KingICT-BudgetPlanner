using BudgetPlanner.Api.Data;
using BudgetPlanner.Api.Entities;
using BudgetPlanner.Api.Repositories.Impl;
using BudgetPlanner.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BudgetPlanner.Api.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly BudgetPlannerDbContext _budgetPlannerDbContext;
        public ExpenseRepository(BudgetPlannerDbContext budgetPlannerDbContext)
        {
            this._budgetPlannerDbContext = budgetPlannerDbContext;
        }

        public async Task<Expense> AddExpense(ExpenseDto expenseDto)
        {
            if (expenseDto == null)

                return null;

            var expenseCategory = new ExpenseCategory
            {
                Id = expenseDto.CategoryId,
                Name = expenseDto.CategoryName
            };

            var expense = new Expense
            {
                Id = expenseDto.Id,
                Month = expenseDto.Month,
                Year = expenseDto.Year,
                Amount = expenseDto.Amount,
                CategoryId = expenseDto.CategoryId,
                IsPlanned = expenseDto.IsPlanned,
                ExpenseCategory = expenseCategory
            };

            var result = await this._budgetPlannerDbContext.Expenses.AddAsync(expense);
            await this._budgetPlannerDbContext.SaveChangesAsync();
            return result.Entity;

            //var expenseNew = await (from expense in this._budgetPlannerDbContext.Expenses
            //                        where expense.Id == expenseDto.Id
            //                        select new ExpenseDto
            //                        {
            //                            Id = expenseDto.Id,
            //                            Month = expenseDto.Month,
            //                            Year = expenseDto.Year,
            //                            CategoryId = expenseDto.CategoryId,
            //                            CategoryName = expenseDto.CategoryName,
            //                            Amount = expenseDto.Amount,
            //                            IsPlanned = expenseDto.IsPlanned
            //                        }).SingleOrDefaultAsync();
            //if (expenseNew != null)
            //{
            //    var result = await this._budgetPlannerDbContext.Expenses.AddAsync(expenseNew);
            //    await this._budgetPlannerDbContext.SaveChangesAsync();
            //    return result.Entity;
            //}

            //return null;

        }

        public async Task<IEnumerable<ExpenseCategory>> GetCategories()
        {
            var categories = await this._budgetPlannerDbContext.ExpenseCategory.ToListAsync();
            return categories;
        }

        public async Task<ExpenseCategory> GetCategory(int id)
        {
            var expenseCategory = await this._budgetPlannerDbContext.ExpenseCategory.SingleOrDefaultAsync(e => e.Id == id);
            return expenseCategory;
        }

        public async Task<Expense> GetExpense(int id)
        {
            var expense = await this._budgetPlannerDbContext.Expenses.Include(e => e.ExpenseCategory).SingleOrDefaultAsync(e => e.Id == id);
            return expense;
        }

        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            var expenses = await this._budgetPlannerDbContext.Expenses.Include(e => e.ExpenseCategory).ToListAsync();
            return expenses;
        }

        public async Task<Expense> UpdateExpense(ExpenseDto expenseToEdit)
        {
            var expense = await _budgetPlannerDbContext.Expenses.FindAsync(expenseToEdit.Id);

            if (expense != null)
            {
                expense.Month = expenseToEdit.Month;
                expense.Year = expenseToEdit.Year;
                expense.Amount = expenseToEdit.Amount;

                await _budgetPlannerDbContext.SaveChangesAsync();
                return expense;
            }

            return null;
           
        }

        public async Task<bool> DeleteExpense(int id)
        {
            var expense = await _budgetPlannerDbContext.Expenses.FindAsync(id);
            if (expense == null)
                return false;

            _budgetPlannerDbContext.Expenses.Remove(expense);
            await _budgetPlannerDbContext.SaveChangesAsync();
            return true;
        }
    }
}
