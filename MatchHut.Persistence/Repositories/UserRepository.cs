using MatchHut.Core.Models;
using MatchHut.Core.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace MatchHut.Persistence.Repositories
{
    public class UserRepository : Repository<User>
    , IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
        : base(context)
        {
        }

        public User GetByUsername(string username)
        {
            return Context.Set<User>().SingleOrDefault(u => u.UserName == username);
        }

        public User GetByEmail(string email)
        {
            return Context.Set<User>().SingleOrDefault(u => u.Email == email);
        }
    }
}