namespace Booking.Core.Domain.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Destination { get; set; }
        public DateTime Date { get; set; }
    }
}
