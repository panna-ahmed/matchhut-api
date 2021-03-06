using MatchHut.Core.Models;

namespace MatchHut.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsername(string username);
        User GetByEmail(string email);
    }
}