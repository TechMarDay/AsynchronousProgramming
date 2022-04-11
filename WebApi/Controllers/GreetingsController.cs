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
            //Console.WriteLine($"Thread before the await: {Thread.CurrentThread.ManagedThreadId}");
            //await Task.Delay(500);
            //Console.WriteLine($"Thread after the await: {Thread.CurrentThread.ManagedThreadId}");
            Random rnd = new Random();
            int number = rnd.Next(1, 10);
            await Task.Delay(number * 1000);
            return $"Hello, {name}! with deplay time: {number * 1000}";
        }
    }
}
