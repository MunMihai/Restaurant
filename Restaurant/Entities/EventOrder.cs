namespace Restaurant.Entities;

public class EventOrder
    {
        public int Id { get; set; }
        public EventType EventType { get; set; }
        public DateTime EventDateTime { get; set; }
        public int NumberOfPeople { get; set; }
        public List<VeganMenuItem> VeganMenuItems { get; set; }
        public List<GlutenFreeMenuItem> GlutenFreeMenuItems { get; set; }
        public List<SpicyMenuItem> SpicyMenuItems { get; set; }
    }