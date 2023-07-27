using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public interface IOrderRepository
    {

        IEnumerable<OrderU> GetAll();

        Task<OrderU> GetById(int id);

        int Add(OrderU @order);

        void Update(OrderU @order);

        void Delete(OrderU @order);
    }
}
