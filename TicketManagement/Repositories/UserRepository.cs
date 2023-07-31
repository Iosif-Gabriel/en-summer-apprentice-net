using Microsoft.EntityFrameworkCore;
using TicketManagement.Exceptions;
using TicketManagement.Models;
using TicketManagement.Repositories.Interfaces;

namespace TicketManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PracticaEndava2Context _dbContext;

        public UserRepository()
        {
            _dbContext = new PracticaEndava2Context();
        }
        public int Add(UserU user)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserU user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserU> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserU> GetById(int id)
        {
            var @order = await _dbContext.UserUs.Where(e => e.IdUser == id).FirstOrDefaultAsync();


            if (@order == null)
                throw new EntityNotFoundException(id, nameof(OrderU));

            return @order;
        }

        public void Update(UserU user)
        {
            throw new NotImplementedException();
        }
    }
}
