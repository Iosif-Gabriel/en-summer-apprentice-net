using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketManagement.Models;
using TicketManagement.Exceptions;

namespace TicketManagement.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly PracticaEndava2Context _dbContext;
       

        public EventRepository()
        {
            _dbContext = new PracticaEndava2Context();
        }

        public int Add(EventU @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(EventU @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }



        public IEnumerable<EventU> GetAll()
        {
            var events = _dbContext.EventUs.Include(e => e.IdVenueNavigation)
                .Include(e => e.IdEventTypeNavigation).Include(e=>e.TicketCategories);
          
            return events;
        }

        public async Task<EventU> GetById(int id)
        {
            var @event = await _dbContext.EventUs.Include(e => e.IdVenueNavigation)
                .Include(e => e.IdEventTypeNavigation).Where(e => e.Idevent == id).Include(e => e.TicketCategories).FirstOrDefaultAsync();

            if (@event == null)
                throw new EntityNotFoundException(id, nameof(EventU));

            return @event;
        }

        public void Update(EventU @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

      
    }
}
