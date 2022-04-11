using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TaskWhenAll
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public TaskWhenAll(string apiURL)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task DemoTaskWhenAllAsync()
        {
            var cards = GetCards(250);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await ProcessCardsVersion1Async(cards);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
        }

        private async Task ProcessCardsVersion1Async(List<string> cards)
        {
            var tasks = new List<HttpResponseMessage>();

            foreach (var card in cards)
            {
                var json = JsonConvert.SerializeObject(card);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var responseTask = httpClient.PostAsync($"{apiURL}/cards", content);
                var task = await responseTask;
                tasks.Add(task);
            }
        }

        private async Task ProcessCardsVersion2Async(List<string> cards)
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
