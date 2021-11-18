namespace TransactionLoggerEmulator.Services
{
	public interface IStateService
	{
		Counters GetCounters();

		void OnTransactionsReceived();

		void OnMultiSearchTransactionReceived();

		void ResetReceived();
		
		void SetTransactionAutoResponse(bool isOk, int delayInMilliseconds);
		void SetMultiSearchTransactionsAutoResponse(bool isOk, int delayInMilliseconds);

		EndpointAutoResponse GetTransactionAutoResponse();
		EndpointAutoResponse GetMultiSearchTransactionsAutoResponse();
	}
}
