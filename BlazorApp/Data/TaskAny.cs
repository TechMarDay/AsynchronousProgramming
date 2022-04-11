using System.Diagnostics;

namespace BlazorApp.Data
{
    public class TaskAny
    {
        private readonly string apiURL = "https://localhost:7222/api";
        private readonly HttpClient httpClient = new HttpClient();

        public async Task DemoTaskAny(CancellationTokenSource cancellationTokenSource)
        {
            var token = cancellationTokenSource.Token;
            var names = new string[] { "js", "c#", "Reactjs", "Java" };

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            // Example 1
            //var tasksHTTP = names.Select(x => GetGreeting(x, token));
            //var task = await Task.WhenAny(tasksHTTP);
            //var content = await task;
            //Console.WriteLine(content.ToUpper());
            //cancellationTokenSource.Cancel();
            //Console.WriteLine($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");

            //Example 2
            var tasksHTTP = names.Select(x =>
            {
                Func<CancellationToken, Task<string>> function = (ct) => GetGreeting(x, ct);
                return function;
            });

            var content = await OnlyOne(tasksHTTP);
            Console.WriteLine(content.ToUpper());
            Console.WriteLine($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
        }

        private async Task<string> GetGreeting(string name, CancellationToken cancellationToken)
        {
            using (var response = await httpClient.GetAsync($"{apiURL}/greetings/delay/{name}", cancellationToken))
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return content;
            }

        }

        private async Task<T> OnlyOne<T>(IEnumerable<Func<CancellationToken, Task<T>>> functions)
        {
            var cts = new CancellationTokenSource();
            var tasks = functions.Select(funcion => funcion(cts.Token));
            var task = await Task.WhenAny(tasks);
            cts.Cancel();
            return await task;
        }

    }
}
