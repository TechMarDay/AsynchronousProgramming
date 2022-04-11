using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace BlazorApp.Data
{
    public class NoneTaskWhenAll
    {
        private readonly string apiURL = "https://localhost:7222/api";
        private readonly HttpClient httpClient = new HttpClient();

        public NoneTaskWhenAll()
        {
        }

        public async Task DemoNoneTaskWhenAllAsync()
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
            foreach (var card in cards)
            {
                var json = JsonConvert.SerializeObject(card);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await httpClient.PostAsync($"{apiURL}/cards", content);
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
