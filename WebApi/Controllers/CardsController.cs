using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardsController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> ProcessCard([FromBody] string card)
        {
            Random random = new Random();
            var randomValue = random.Next(0, 10);
            var approved = randomValue > 5;
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine($"Card {card} processed: {approved}");
            return Ok(new { Card = card, Approved = approved });
        }
    }
}
