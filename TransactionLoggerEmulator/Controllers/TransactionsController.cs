using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionLoggerEmulator.Services;

namespace TransactionLoggerEmulator.Controllers
{
	[ApiController]
	[Route("public")]
	public class TransactionsController : ControllerBase
	{
		private readonly ILogger<TransactionsController> _logger;
		private readonly IStateService _state;

		public TransactionsController(ILogger<TransactionsController> logger, IStateService state)
		{
			_logger = logger;
			_state = state;
		}
		
		[HttpPost]
		[Route("transaction")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> LogTransaction()
		{
			var autoResponse = _state.GetTransactionAutoResponse();

			var timeout = autoResponse.DelayInMilliseconds;

			if (timeout > 0)
			{
				await Task.Delay(timeout);
			}

			if (!autoResponse.IsOk)
			{
				return NotFound();
			}
			_state.OnTransactionsReceived();
			return NoContent();
		}

		[HttpPost]
		[Route("transactions")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> LogTransactions()
		{
			var autoResponse = _state.GetMultiSearchTransactionsAutoResponse();

			var timeout = autoResponse.DelayInMilliseconds;

			if (timeout > 0)
			{
				await Task.Delay(timeout);
			}

			if (!autoResponse.IsOk)
			{
				return NotFound();
			}

			_state.OnMultiSearchTransactionReceived();
			return NoContent();
		}
	}
}
