using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform.Data
{
    public class Cancelling_Tasks
    {
        private readonly string apiURL;
        private CancellationTokenSource cancellationTokenSource;
        private readonly HttpClient httpClient;

        public Cancelling_Tasks(string apiURL, CancellationTokenSource cancellationTokenSource)
        {
            this.apiURL = apiURL;
            this.cancellationTokenSource = cancellationTokenSource;
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click()
        {
            var cards = GetCards(50);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await ProcessCardAsync(cards, cancellationTokenSource.Token);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }

            MessageBox.Show($"Operation finalized in {stopwatch.ElapsedMilliseconds / 1000.0} seconds");

        }

        private async Task ProcessCardAsync(List<string> cards, CancellationToken token = default)
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
