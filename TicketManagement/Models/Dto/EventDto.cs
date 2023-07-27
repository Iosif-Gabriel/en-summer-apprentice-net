namespace TicketManagement.Models.Dto
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;

        public string EventDescription { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;

        public string Venue { get; set; }
    }
}
