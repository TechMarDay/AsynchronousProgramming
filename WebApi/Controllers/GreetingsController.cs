using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/greetings")]
    [ApiController]
    public class GreetingsController : ControllerBase
    {
        [HttpGet("{name}")]
        public ActionResult<string> GetGreeting(string name)
        {
            return $"Hello, {name}!";
        }

        [HttpGet("delay/{name}")]
        public async Task<ActionResult<string>> GetGreetingAsync(string name)
        {
            //There is no synchronization context in .net core, .net 5, .net 6.
            //Console.WriteLine($"Thread before the await: {Thread.CurrentThread.ManagedThreadId}");
            //await Task.Delay(1000);

            //Console.WriteLine($"Thread after the await: {Thread.CurrentThread.ManagedThreadId}");

            Random rnd = new Random();
            int number = rnd.Next(1, 10);
            await Task.Delay(number * 1000);
            return $"Hello, {name}! with deplay time: {number * 1000}";

            //return $"Hello, {name}!";
        }

        [HttpGet("goodbye/{name}")]
        public async Task<ActionResult<string>> GetGoodbye(string name)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 10);
            await Task.Delay(number * 1000);
            return $"Bye, {name}! with deplay time: {number * 1000}";
        }

        [HttpGet("delay/asyncvoid/{name}")]
        public async Task<ActionResult<string>> GetGreeting1Async(string name)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 10);
            await Task.Delay(TimeSpan.FromSeconds(number));

            // The try catch does not avoid the crashing of the app
            //try
            //{
            //    AsyncVoidMethod();
            //}
            //catch (Exception ex)
            //{

            //}

            await AsyncTaskMethod();
            //SyncVoidMethod();

            return $"Hello, {name}!";
        }

        // Antipattern: do not use async void
        private async void AsyncVoidMethod()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new Exception();
        }

        private void SyncVoidMethod()
        {
            throw new ApplicationException();
        }

        private async Task AsyncTaskMethod()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new ApplicationException();
        }
    }
}
