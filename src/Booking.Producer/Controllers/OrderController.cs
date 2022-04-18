using Booking.Core.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publisher;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, IPublishEndpoint publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
        {
            if (ticket != null)
            {
                ticket.Id = Guid.NewGuid();
                ticket.Date = DateTime.Now;

                await _publisher.Publish<Ticket>(ticket);
                _logger.LogInformation($"Send {nameof(Ticket)}");

                return Ok();
            }

            return BadRequest();
        }
    }
}
