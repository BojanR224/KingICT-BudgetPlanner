using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetPlanner.Api.Entities
{
    public class Income
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
        public IncomeCategory Category { get; set; }


        public Income()
        {
            // Set default values
            Id = 0;
            Month = DateTime.Now.ToString("MMMM");
            Year = DateTime.Now.Year;
            CategoryId = 1; // Set appropriate default category
            Amount = 0.0f;
            IsPlanned = false;
        }
    }


}
