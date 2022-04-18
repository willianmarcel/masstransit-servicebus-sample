using Booking.Core.Domain.Entities;
using MassTransit;

namespace Booking.Consumer.Consumers
{
    public class TicketConsumer : IConsumer<Ticket>
    {
        private readonly ILogger<TicketConsumer> _logger;

        public TicketConsumer(ILogger<TicketConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Ticket> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name);
            _logger.LogInformation($"Nova mensagem recebida: {context.Message.Name} - {context.Message.Destination}");
        }
    }
}
