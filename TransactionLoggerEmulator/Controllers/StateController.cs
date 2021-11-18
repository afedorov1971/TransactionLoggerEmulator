using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionLoggerEmulator.Services;

namespace TransactionLoggerEmulator.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StateController : ControllerBase
	{
		private readonly ILogger<StateController> _logger;
		private readonly IStateService _state;

		public StateController(ILogger<StateController> logger, IStateService state)
		{
			_logger = logger;
			_state = state;
		}

		[HttpGet]
		[Route("counters")]
		[ProducesResponseType(200, Type=typeof(Counters))]
		public IActionResult GetCounters()
		{
			return Ok(_state.GetCounters());
		}

		[HttpPost]
		[Route("clear/counters")]
		[ProducesResponseType(204)]
		public IActionResult ClearCounters()
		{
			_state.ResetReceived();
			return NoContent();
		}

		[HttpPost]
		[Route("response/transaction")]
		[ProducesResponseType(204)]
		public IActionResult SetTransactionResponse(bool isOk, int delayInMilliseconds)
		{
			_state.SetTransactionAutoResponse(isOk, delayInMilliseconds);
			return NoContent();
		}

		[HttpGet]
		[Route("response/transaction")]
		[ProducesResponseType(200, Type = typeof(EndpointAutoResponse))]
		public IActionResult GetTransactionResponse()
		{
			return Ok(_state.GetTransactionAutoResponse());
		}

		[HttpPost]
		[Route("response/transactions")]
		[ProducesResponseType(204)]
		public IActionResult SetTransactionsResponse(bool isOk, int delayInMilliseconds)
		{
			_state.SetMultiSearchTransactionsAutoResponse(isOk, delayInMilliseconds);
			return NoContent();
		}

		[HttpGet]
		[Route("response/transactions")]
		[ProducesResponseType(200, Type = typeof(EndpointAutoResponse))]
		public IActionResult GetTransactionsResponse()
		{
			return Ok(_state.GetMultiSearchTransactionsAutoResponse());
		}
	}
}
