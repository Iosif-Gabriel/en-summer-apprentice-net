namespace TicketManagement.Models.Dto
{
    public class OrderDto
    {
        public int IdOrder { get; set; }

        public int IdUser { get; set; }

        public int IdTicketCategory { get; set; }

        public DateTime OrderedAt { get; set; }

        public int NumberOfTickets { get; set; }

        public double TotalPrice { get; set; }

        public virtual TicketCategoryDto IdTicketCategoryNavigation { get; set; }

        public virtual string IdUserNavigation { get; set; } = null!;
    }
}
