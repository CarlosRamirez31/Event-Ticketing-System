using EventTicketing.Application.Features.Events.Commands;

namespace EventTicketing.Application.Domain.Entities
{
    public class Event
    {
        public Event(int eventId, string name, DateTime startDate, DateTime endDate, string location, string eventCreatedBy)
        {
            EventId = eventId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            EventCreatedBy = eventCreatedBy;
        }

        public int EventId { get; set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Location { get; private set; }
        public string EventCreatedBy { get; private set; }
        public ICollection<EventPrice> EventPrices { get; set; } = new HashSet<EventPrice>();

        public void UpdateEvent(UpdateEvent.UpdateEventCommand command)
        {
            EventId = command.EventId;
            Name = command.Name;
            StartDate = command.StartDate;
            EndDate = command.EndDate;
            Location = command.Location;
            EventCreatedBy = command.EventCreatedBy;
        }
    }
}
