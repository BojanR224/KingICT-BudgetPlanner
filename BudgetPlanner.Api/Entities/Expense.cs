using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BudgetPlanner.Api.Entities
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Month { get; set; } 

        public int Year { get; set; }

        public int CategoryId { get; set; }

        public float Amount { get; set; }

        public bool IsPlanned { get; set; }

        [ForeignKey("CategoryId")]
        public ExpenseCategory ExpenseCategory { get; set; }

    }
}
