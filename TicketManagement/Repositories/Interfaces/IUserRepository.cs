using TicketManagement.Models;

namespace TicketManagement.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserU> GetAll();

        Task<UserU> GetById(int id);

        int Add(UserU @user);

        void Update(UserU @user);

        void Delete(UserU @user);
    }
}
