using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Repositories;
using System.Linq;

namespace HotelManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(HotelDbContext context) : base(context)
        {
        }

        public User? GetByUsername(string username)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username);
        }
    }
}
