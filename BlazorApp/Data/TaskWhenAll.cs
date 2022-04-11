using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace BlazorApp.Data
{
    public class TaskWhenAll
    {
        private readonly string apiURL = "https://localhost:7222/api";
        private readonly HttpClient httpClient = new HttpClient();

        public async Task DemoTaskWhenAllAsync()
        {
            var cards = GetCards(5);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await ProcessCardsAsync(cards);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
        }

        private async Task ProcessCardsAsync(List<string> cards)
        {
            var tasks = new List<Task<HttpResponseMessage>>();

            foreach (var card in cards)
            {
                var json = JsonConvert.SerializeObject(card);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var responseTask = httpClient.PostAsync($"{apiURL}/cards", content);
                tasks.Add(responseTask);
            }

            await Task.WhenAll(tasks);
        }

        private List<string> GetCards(int amountOfCards)
        {
            var cards = new List<string>();

            for (int i = 0; i < amountOfCards; i++)
            {
                cards.Add(i.ToString().PadLeft(16, '0'));
            }

            return cards;
        }
    }
}
