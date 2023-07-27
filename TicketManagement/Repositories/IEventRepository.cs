using TicketManagement.Models;

namespace TicketManagement.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<EventU> GetAll();

        Task<EventU> GetById(int id);

        int Add(EventU @event);

        void Update(EventU @event);

        void Delete(EventU @event);

    
    }
}
