using BudgetPlanner.Api.Entities;
using BudgetPlanner.Api.Extensions;
using BudgetPlanner.Api.Repositories.Impl;
using BudgetPlanner.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository incomeRepository;

        public IncomeController(IIncomeRepository incomeRepository)
        {
            this.incomeRepository = incomeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncomeDto>>> GetIncomes()
        {
            try
            {
                var incomes = await this.incomeRepository.GetIncomes();

                if (incomes != null)
                {
                    return Ok(incomes);
                }

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IncomeDto>> GetIncome(int id)
        {
            try
            {
                var income = await this.incomeRepository.GetIncome(id);

                if (income != null)
                {
                    return Ok(income);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<IncomeCategoryDto>>> GetCategories()
        {
            try
            {
                var categories = await this.incomeRepository.GetCategories();

                if (categories != null)
                {
                    return Ok(categories);
                }

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("Category/{name}")]
        public async Task<ActionResult<IncomeCategory>> GetCategory(string name)
        {
            try
            {
                var category = await this.incomeRepository.GetCategory(name);

                if (category != null)
                {
                    return Ok(category);
                }

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<IncomeDto>> AddIncome([FromBody] IncomeDto income)
        {
            if (income == null || income.CategoryId <= 0)
            {
                return BadRequest("Invalid income data. Please check the category and income details.");
            }

            try
            {
                var newIncome = await this.incomeRepository.AddIncome(income);
                if (newIncome != null)
                {
                    return Ok(newIncome.ConvertToDto());
                }

                return BadRequest("Failed to add income. Please check your input.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<IncomeDto>> UpdateIncome(IncomeEditDto income)
        {
            try
            {
                var updatedIncome = await this.incomeRepository.UpdateIncome(income);

                if (updatedIncome != null)
                {
                    return Ok(updatedIncome);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteIncome(int id)
        {
            try
            {
                var result = await this.incomeRepository.DeleteIncome(id);

                if (result)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
