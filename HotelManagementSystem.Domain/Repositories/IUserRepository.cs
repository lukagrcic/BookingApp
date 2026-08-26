using HotelManagementSystem.Domain.Entities;

namespace HotelManagementSystem.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByUsername(string username);
    }
}
