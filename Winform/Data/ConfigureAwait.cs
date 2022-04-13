using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform
{
    public class ConfigureAwait
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public ConfigureAwait(string apiURL)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnStart_Click(Button btnCancel)
        {
            //SynchronizationContext is guaranted the same UI thread
            //btnCancel.Text = "before"; //UI thread

            //Console.WriteLine($"Thread before the await: {Thread.CurrentThread.ManagedThreadId}");

            //await Task.Delay(1000).ConfigureAwait(true);

            //.ConfigureAwait(true); //default
            //.ConfigureAwait(false); //disable the SynchronizationContext

            //Console.WriteLine($"Thread after the await: {Thread.CurrentThread.ManagedThreadId}");


            //var result = await GetGreeting("Peter");

            //btnCancel.Text = "after"; //UI thread

        }

        private async Task<string> GetGreeting(string name)
        {
            using (var response = await httpClient.GetAsync($"{apiURL}/greetings/delay/{name}"))
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}
