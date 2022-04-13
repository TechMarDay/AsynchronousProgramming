using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winform.Data
{
    public class Only_One_Pattern
    {

        private readonly string apiURL;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly HttpClient httpClient;

        public Only_One_Pattern(string apiURL, CancellationTokenSource cancellationTokenSource)
        {
            this.apiURL = apiURL;
            this.cancellationTokenSource = cancellationTokenSource;
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var token = cancellationTokenSource.Token;
            var names = new string[] { "Goku", "Vegeta", "Fizze", "Berrus" };

            // Example 1
            //var tasksHTTPs = names.Select(x => GetGreeting(x, token));
            //var task = await Task.WhenAny(tasksHTTPs);
            //var content = await task;
            //Console.WriteLine(content.ToUpper());
            //Console.WriteLine($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
            //cancellationTokenSource.Cancel();

            // Example 2
            //var tasksHTTP = names.Select(x =>
            //{
            //    Func<CancellationToken, Task<string>> function = (ct) => GetGreeting(x, ct);
            //    return function;
            //});

            //var content = await OnlyOne(tasksHTTP);
            //Console.WriteLine(content.ToUpper());

            // Example 3 for difference functions
            var content = await OnlyOne(
                (ct) => GetGreeting("Goku", ct),
                (ct) => GetGoodbye("Goku", ct));
            Console.WriteLine(content.ToUpper());
        }

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

        private async Task<string> GetGreeting(string name, CancellationToken cancellationToken)
        {
            using (var response = await httpClient.GetAsync($"{apiURL}/greetings/delay/{name}", cancellationToken))
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return content;
            }

        }

        private async Task<string> GetGoodbye(string nombre, CancellationToken cancellationToken)
        {
            using (var response = await httpClient.GetAsync($"{apiURL}/greetings/goodbye/{nombre}", cancellationToken))
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return content;
            }

        }

    }
}
