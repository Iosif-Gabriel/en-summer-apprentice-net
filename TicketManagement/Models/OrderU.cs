using System;
using System.Collections.Generic;

namespace TicketManagement.Models;

public partial class OrderU
{
    public int IdOrder { get; set; }

    public int IdUser { get; set; }

    public int IdTicketCategory { get; set; }

    public DateTime OrderedAt { get; set; }

    public int NumberOfTickets { get; set; }

    public double TotalPrice { get; set; }

    public virtual TicketCategory IdTicketCategoryNavigation { get; set; } = null!;

    public virtual UserU IdUserNavigation { get; set; } = null!;
}
