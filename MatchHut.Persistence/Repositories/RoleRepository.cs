using MatchHut.Core.Models;
using MatchHut.Core.Repositories;

namespace MatchHut.Persistence.Repositories
{
    public class RoleRepository : Repository<Role>
    , IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context)
        : base(context)
        {
        }
    }
}