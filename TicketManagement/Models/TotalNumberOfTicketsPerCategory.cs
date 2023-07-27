using System;
using System.Collections.Generic;

namespace TicketManagement.Models;

public partial class TotalNumberOfTicketsPerCategory
{
    public int IdTicketCategory { get; set; }

    public int? TotalNumberOfTickets { get; set; }

    public decimal? TotalSalesValue { get; set; }
}
