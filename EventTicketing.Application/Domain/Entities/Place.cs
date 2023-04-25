using Microsoft.Identity.Client;

namespace EventTicketing.Application.Domain.Entities
{
    public class Place
    {
        public Place(int placeId, string name, int row, int zoneId)
        {
            PlaceId = placeId;
            Name = name;
            Row = row;
            ZoneId = zoneId;
        }

        public int PlaceId { get; set; }
        public string Name { get; private set; }
        public int Row { get; private set; }
        public int ZoneId { get; private set; }
        public Zone? Zone { get; set; }
        public ICollection<Place> Places { get; set; } = new HashSet<Place>();
        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
