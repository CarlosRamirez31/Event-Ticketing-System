namespace EventTicketing.Application.Domain.Entities
{
    public class Ticket
    {
        public Ticket(int ticketId, int eventPriceId, int placeId, string participantName, DateTime paymentDate)
        {
            TicketId = ticketId;
            EventPriceId = eventPriceId;
            PlaceId = placeId;
            ParticipantName = participantName;
            PaymentDate = paymentDate;
        }

        public int TicketId { get; set; }
        public int EventPriceId { get; private set; }
        public int PlaceId { get; private set; }
        public string ParticipantName { get; private set; } 
        public DateTime PaymentDate { get; private set; }
        public EventPrice? EventPrice { get; set; }
        public Place? Place { get; set; }
    }
}
