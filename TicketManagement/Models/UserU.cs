using System;
using System.Collections.Generic;

namespace TicketManagement.Models;

public partial class UserU
{
    public int IdUser { get; set; }

    public string? Username { get; set; }

    public string? PasswordUser { get; set; }

    public string? Email { get; set; }

    public int? PhoneNumber { get; set; }

    public virtual ICollection<OrderU> OrderUs { get; set; } = new List<OrderU>();
}
