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
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;
        public ExpenseController(IExpenseRepository expenseRepository)
        {
            this._expenseRepository = expenseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpenses()
        {

            try
            {
                var expenses = await this._expenseRepository.GetExpenses();
                var expenseCategories = await this._expenseRepository.GetCategories();

                if (expenses == null || expenseCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    var expenseDtos = expenses.ConvertToDto(expenseCategories);

                    return Ok(expenseDtos);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<ExpenseCategoryDto>>> GetCategories()
        {
            try
            {
                var categories = await this._expenseRepository.GetCategories();

                if (categories == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(categories);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "No expense categories found");

            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExpenseDto>> GetExpense(int id)
        {
            try
            {
                var expense = await this._expenseRepository.GetExpense(id);

                if (expense == null)
                {
                    return NotFound();
                }
                else
                {

                    var expenseDto = expense.ConvertToDto();

                    return Ok(expenseDto);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseDto>> PostItem([FromBody] ExpenseDto expenseDto)
        {
            try
            {

                var newExpense = await this._expenseRepository.AddExpense(expenseDto);

                if (newExpense == null)
                {
                    return NoContent();
                }

                var expenseCategory = await this._expenseRepository.GetCategory(newExpense.CategoryId);

                if (expenseCategory == null)
                {
                    throw new Exception($"Expense category not found with id: {newExpense.CategoryId}");
                }

                var newExpenseDto = newExpense.ConvertToDto();

                return CreatedAtAction(nameof(GetExpense), new { id = newExpenseDto.Id }, newExpenseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseDto expenseDto)
        {
            if (id != expenseDto.Id)
            {
                return BadRequest();
            }

            var result = await _expenseRepository.UpdateExpense(expenseDto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            try
            {
                var expense = await _expenseRepository.GetExpense(id);

                if (expense == null)
                {
                    return NotFound($"Expense with Id = {id} not found");
                }

                var success = await _expenseRepository.DeleteExpense(id);

                if (!success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the expense");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
