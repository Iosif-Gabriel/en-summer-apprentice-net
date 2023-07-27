using System;
using System.Collections.Generic;

namespace TicketManagement.Models;

public partial class Venue
{
    public int IdVenue { get; set; }

    public string VenueLocation { get; set; } = null!;

    public string? VenueType { get; set; }

    public int? VenueCapacity { get; set; }

    public virtual ICollection<EventU> EventUs { get; set; } = new List<EventU>();

   
}
