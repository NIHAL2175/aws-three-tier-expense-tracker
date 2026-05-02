using System.Threading.Tasks;
using Expense_Tracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionApiController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionApiController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("userId query parameter is required.");
            }

            var transactionDtos = await _transactionService.GetTransactionDtosForUserAsync(userId);

            return Ok(transactionDtos);
        }
    }
}
