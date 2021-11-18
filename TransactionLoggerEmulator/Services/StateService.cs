namespace TransactionLoggerEmulator.Services
{
	public class StateService : IStateService
	{
		private int _transactionReceived;
		private int _multiSearchTransactionsReceived;
		private readonly object _lock = new();

		private int _transactionResponseDelay;
		private int _multiSearchTransactionsResponseDelay;
		private bool _isTransactionReplayOk = true;
		private bool _isMultiSearchTransactionsReplayOk = true;

		public Counters GetCounters()
		{
			lock (_lock)
			{
				return new Counters
					{ Transactions = _transactionReceived, MultiSearchTransactions = _multiSearchTransactionsReceived };

			}
		}
		
		public void OnTransactionsReceived()
		{
			lock (_lock)
			{
				_transactionReceived++;
			}
		}

		public void OnMultiSearchTransactionReceived()
		{
			lock (_lock)
			{
				_multiSearchTransactionsReceived++;
			}
		}

		public void ResetReceived()
		{
			lock (_lock)
			{
				_transactionReceived = 0;
				_multiSearchTransactionsReceived = 0;
			}
		}
		
		public void SetTransactionAutoResponse(bool isOk, int delayInMilliseconds)
		{
			lock (_lock)
			{
				_isTransactionReplayOk = isOk;
				_transactionResponseDelay = delayInMilliseconds;
			}
		}

		public void SetMultiSearchTransactionsAutoResponse(bool isOk, int delayInMilliseconds)
		{
			lock (_lock)
			{
				_isMultiSearchTransactionsReplayOk = isOk;
				_multiSearchTransactionsResponseDelay = delayInMilliseconds;
			}
		}

		public EndpointAutoResponse GetTransactionAutoResponse()
		{
			lock (_lock)
			{
				return new EndpointAutoResponse { IsOk = _isTransactionReplayOk, DelayInMilliseconds = _transactionResponseDelay};
			}
		}

		public EndpointAutoResponse GetMultiSearchTransactionsAutoResponse()
		{
			lock (_lock)
			{
				return new EndpointAutoResponse
				{
					IsOk = _isMultiSearchTransactionsReplayOk,
					DelayInMilliseconds = _multiSearchTransactionsResponseDelay
				};
			}
		}

	}
}
