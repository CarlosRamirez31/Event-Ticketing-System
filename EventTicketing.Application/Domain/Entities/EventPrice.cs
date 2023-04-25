namespace EventTicketing.Application.Domain.Entities
{
    public class EventPrice
    {
        public EventPrice(int eventPriceId, int eventId, int zoneId, int price)
        {
            EventPriceId = eventPriceId;
            EventId = eventId;
            ZoneId = zoneId;
            Price = price;
        }

        public int EventPriceId { get; set; }
        public int EventId { get; private set; }
        public int ZoneId { get; private set; }
        public double Price { get; private set; }
        public Event? Event { get; set; }
        public Zone? Zone { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
