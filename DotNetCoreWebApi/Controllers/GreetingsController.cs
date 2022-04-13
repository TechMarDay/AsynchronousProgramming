using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Controllers
{
    [Route("api/greetings")]
    [ApiController]
    public class GreetingsController : ControllerBase
    {
        [HttpGet("delay/{name}")]
        public async Task<ActionResult<string>> GetGreetingAsync(string name)
        {
            //There is no synchronization context in .net core, .net 5, but support in .net 6.
            Console.WriteLine($"Thread before the await: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(500).ConfigureAwait(true);
            Console.WriteLine($"Thread after the await: {Thread.CurrentThread.ManagedThreadId}");
            return $"Hello, {name}!";
        }
    }
}
