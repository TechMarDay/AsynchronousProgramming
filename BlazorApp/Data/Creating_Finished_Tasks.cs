namespace BlazorApp.Data
{
    public class Creating_Finished_Tasks
    {
        public Task ProcessCardsMock(List<string> cards,
            IProgress<int> progress = null,
            CancellationToken cancellationToken = default)
        {

            // ...

            return Task.CompletedTask;
        }

        public Task<List<string>> GetCardsMock(int amountOfCards,
           CancellationToken cancellationToken = default)
        {
            var cards = new List<string>();
            cards.Add("0000000001");

            return Task.FromResult(cards);
        }

        public Task GetTaskWithError()
        {
            return Task.FromException(new ApplicationException());
        }

        public Task GetCancelledTask()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            return Task.FromCanceled(cancellationTokenSource.Token);
        }
    }
}
