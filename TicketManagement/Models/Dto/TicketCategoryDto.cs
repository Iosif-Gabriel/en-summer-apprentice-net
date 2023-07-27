namespace TicketManagement.Models.Dto
{
    public class TicketCategoryDto
    {
        public int IdTicketCategory { get; set; }

        public int? IdEvent { get; set; }

        public string? DescriptionEventCategory { get; set; }

        public decimal? Price { get; set; }
    }
}
