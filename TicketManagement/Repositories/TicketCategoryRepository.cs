using Microsoft.EntityFrameworkCore;
using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly PracticaEndava2Context _dbContext;


        public TicketCategoryRepository()
        {
            _dbContext = new PracticaEndava2Context();
        }

        public int Add(TicketCategory @ticketCategory)
        {
            throw new NotImplementedException();
        }

        public void Delete(TicketCategory @ticketCategory)
        {
            _dbContext.Remove(@ticketCategory);
            _dbContext.SaveChanges();
        }



        public IEnumerable<TicketCategory> GetAll()
        {
            var @ticketCategory = _dbContext.TicketCategories;

            return @ticketCategory;
        }

        public async Task<TicketCategory> GetById(int id)
        {
            var @ticketCategory = await _dbContext.TicketCategories
             .Where(e => e.IdTicketCategory == id).FirstOrDefaultAsync();

            return @ticketCategory;
        }

        public void Update(TicketCategory @ticketCategory)
        {
            _dbContext.Entry(@ticketCategory).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
