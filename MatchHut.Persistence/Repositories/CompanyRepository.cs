using MatchHut.Core.Models;
using MatchHut.Core.Repositories;

namespace MatchHut.Persistence.Repositories
{
    public class CompanyRepository : Repository<Company>
    , ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context)
        : base(context)
        {
        }
    }
}