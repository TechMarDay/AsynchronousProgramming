namespace BlazorApp.Data
{
    public class RetryPattern
    {
        private async Task Retry(Func<Task> f, int retryTimes = 3, int waitTime = 500)
        {
            for (int i = 0; i < retryTimes; i++)
            {
                try
                {
                    await f();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.Delay(waitTime);
                }
            }
        }

        private async Task<T> Retry<T>(Func<Task<T>> f, int retryTimes = 3, int waitTime = 500)
        {
            for (int i = 0; i < retryTimes - 1; i++)
            {
                try
                {
                    return await f();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.Delay(waitTime);
                }
            }

            return await f();
        }
    }
}
