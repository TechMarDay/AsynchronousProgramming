using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform.Data
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

        public async Task btnStart_Click()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                //await ProcessAsync();

                //await DemoTaskWhenAllWithLoopAsync();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
        }

        public async Task ProcessAsync()
        {

            //await Function1Async();
            //await Function2Async();
            //await Function3Async();


            var function1Task = Function1Async();
            var function2Task = Function2Async();
            var function3Task = Function3Async();
            await Task.WhenAll(function1Task, function2Task, function3Task);

        }

        public async Task Function1Async()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        public async Task Function2Async()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
        }

        public async Task Function3Async()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
        }

        public async Task DemoTaskWhenAllWithLoopAsync()
        {
            var cards = GetCards(20);
            try
            {
                await ProcessCardsAsync(cards);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
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
