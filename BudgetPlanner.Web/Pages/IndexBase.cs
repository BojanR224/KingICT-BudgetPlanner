using BudgetPlanner.Models.Dtos;
using BudgetPlanner.Web.Services.Impl;
using Microsoft.AspNetCore.Components;
using System.Data;
using System.Runtime.InteropServices;

namespace BudgetPlanner.Web.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IIncomeService IncomeService { get; set; }

        [Inject]
        public IExpenseService ExpenseService { get; set; }

        public IEnumerable<IncomeDto> Incomes { get; set; } = new List<IncomeDto>();

        public IEnumerable<IncomeCategoryDto> IncomeCategories { get; set; } = new List<IncomeCategoryDto>();

        public IEnumerable<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();

        public IEnumerable<ExpenseCategoryDto> ExpenseCategories { get; set; } = new List<ExpenseCategoryDto>();


        public IEnumerable<String> AvailableMonths { get; set; } = new List<String>();

        public Dictionary<string, Tuple<float,float>> IncomeTable { get; set; } = new Dictionary<string, Tuple<float, float>>();


        protected override async Task OnInitializedAsync()
        {
            Incomes = await IncomeService.GetIncomes();
            IncomeCategories = await IncomeService.GetCategories();
            Expenses = await ExpenseService.GetExpenses();
            ExpenseCategories = await ExpenseService.GetCategories();

            AvailableMonths = Incomes.ToList().Select(i => i.Month.ToString()).Distinct();

            IncomeTable = AvailableMonths.ToDictionary(m => m, m => new Tuple<float, float>
            (
                Incomes.ToList().Where(i => i.Month == m && i.IsPlanned == false).Select(s => s.Amount).ToList().Sum(),
                Incomes.ToList().Where(i => i.Month == m && i.IsPlanned == true).Select(s => s.Amount).ToList().Sum()
            ));

        }

        public IndexBase()
        {
        }
    }
}
