using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace BlazorApp.Data
{
    public class Cancelling_Tasks
    {
        private readonly string apiURL = "https://localhost:7222/api";
        private readonly HttpClient httpClient = new HttpClient();

        public async Task DemoCancellingTask(CancellationTokenSource cancellationTokenSource)
        {
            var cards = GetCards(250);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await ProcessCardsAsync(cards, cancellationTokenSource.Token);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
        }

        private async Task ProcessCardsAsync(List<string> cards, CancellationToken token = default)
        {
            foreach (var card in cards)
            {
                var json = JsonConvert.SerializeObject(card);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await httpClient.PostAsync($"{apiURL}/cards", content, token);
            }
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
