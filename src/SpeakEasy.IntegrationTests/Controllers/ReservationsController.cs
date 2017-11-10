using Microsoft.AspNetCore.Mvc;

namespace SpeakEasy.IntegrationTests.Controllers
{
    [Route("api/products/{productId}/reservations")]
    public class ReservationsController : Controller
    {
        [HttpPost]
        public IActionResult Post(int productId)
        {
            if (productId == 1)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        public IActionResult Put(int productId, int priceIncrease)
        {
            if (priceIncrease > 100)
            {
                return StatusCode(201);
            }

            return BadRequest();
        }
    }
}
