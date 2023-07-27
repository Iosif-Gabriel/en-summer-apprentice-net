using System;
using System.Collections.Generic;

namespace TicketManagement.Models;

public partial class TicketCategory
{
    public int IdTicketCategory { get; set; }

    public int? IdEvent { get; set; }

    public string? DescriptionEventCategory { get; set; }

    public decimal? Price { get; set; }

    public virtual EventU? IdEventNavigation { get; set; }

    public virtual ICollection<OrderU> OrderUs { get; set; } = new List<OrderU>();
}
