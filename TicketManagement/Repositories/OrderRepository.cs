using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManagement.Exceptions;
using TicketManagement.Models;
using TicketManagement.Repositories.Interfaces;

namespace TicketManagement.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PracticaEndava2Context _dbContext;


        public OrderRepository()
        {
            _dbContext = new PracticaEndava2Context();
        }

        public void Add(OrderU @order)
        {
            _dbContext.OrderUs.Add(@order);
            _dbContext.SaveChanges();
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
            var @order = await _dbContext.OrderUs.Where(e => e.IdOrder == id).
                Include(e => e.IdTicketCategoryNavigation).FirstOrDefaultAsync();

            
            if (@order == null)
                throw new EntityNotFoundException(id, nameof(OrderU));

            return @order;
        }

        public void Update(OrderU @order)
        {
            _dbContext.Entry(@order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
