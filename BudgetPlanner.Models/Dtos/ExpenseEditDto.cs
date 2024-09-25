using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Models.Dtos
{
    public class ExpenseEditDto
    {
        public int Id { get; set; }

        public string Month { get; set; }

        public int Year { get; set; }

        public float Amount { get; set; }
    }
}
