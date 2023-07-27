using Microsoft.EntityFrameworkCore;
using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PracticaEndava2Context _dbContext;


        public OrderRepository()
        {
            _dbContext = new PracticaEndava2Context();
        }

        public int Add(OrderU @order)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderU @order)
        {
            _dbContext.Remove(@order);
            _dbContext.SaveChanges();
        }



        public IEnumerable<OrderU> GetAll()
        {
            
            var @order = _dbContext.OrderUs.Include(e => e.IdUserNavigation)
                .Include(e=>e.IdTicketCategoryNavigation);

            return @order;
        }

        public async Task<OrderU> GetById(int id)
        {
            var @order = await _dbContext.OrderUs.Where(e => e.IdOrder == id).FirstOrDefaultAsync();

            return @order;
        }

        public void Update(OrderU @order)
        {
            _dbContext.Entry(@order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
