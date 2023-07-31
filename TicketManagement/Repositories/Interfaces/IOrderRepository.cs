using TicketManagement.Models;

namespace TicketManagement.Repositories.Interfaces
{
    public interface IOrderRepository
    {

        IEnumerable<OrderU> GetAll();

        Task<OrderU> GetById(int id);

        void Add(OrderU @order);

        void Update(OrderU @order);

        void Delete(OrderU @order);
    }
}
