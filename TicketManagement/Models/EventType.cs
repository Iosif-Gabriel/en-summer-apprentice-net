using System;
using System.Collections.Generic;

namespace TicketManagement.Models;

public partial class EventType
{
    public int IdEventType { get; set; }

    public string? EventTypeName { get; set; }

    public virtual ICollection<EventU> EventUs { get; set; } = new List<EventU>();
}
