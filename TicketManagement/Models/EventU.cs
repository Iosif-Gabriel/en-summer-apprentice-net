using System;
using System.Collections.Generic;

namespace TicketManagement.Models;

public partial class EventU
{
    public int Idevent { get; set; }

    public int? IdVenue { get; set; }

    public int? IdEventType { get; set; }

    public string? EventName { get; set; }

    public string? DescriptionEvent { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual EventType? IdEventTypeNavigation { get; set; }

    public virtual Venue? IdVenueNavigation { get; set; }

    public virtual ICollection<TicketCategory> TicketCategories { get; set; } = new List<TicketCategory>();
}
