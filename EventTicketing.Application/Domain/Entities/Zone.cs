namespace EventTicketing.Application.Domain.Entities
{
    public class Zone
    {
        public Zone(int zoneId, string name)
        {
            ZoneId = zoneId;
            Name = name;
        }

        public int ZoneId { get; set; }
        public string Name { get; private set; }
        public ICollection<EventPrice> EventPrices { get; set; } = new HashSet<EventPrice>();
        public ICollection<Place> Places { get; set; } = new HashSet<Place>();
    }
}
