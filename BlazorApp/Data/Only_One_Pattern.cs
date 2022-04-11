namespace BlazorApp.Data
{
    public class Only_One_Pattern
    {
        private async Task<T> OnlyOne<T>(IEnumerable<Func<CancellationToken, Task<T>>> functions)
        {
            var cts = new CancellationTokenSource();
            var tasks = functions.Select(funcion => funcion(cts.Token));
            var task = await Task.WhenAny(tasks);
            cts.Cancel();
            return await task;
        }

        private async Task<T> OnlyOne<T>(params Func<CancellationToken, Task<T>>[] functions)
        {
            var cts = new CancellationTokenSource();
            var tasks = functions.Select(funcion => funcion(cts.Token));
            var task = await Task.WhenAny(tasks);
            cts.Cancel();
            return await task;
        }
    }
}
