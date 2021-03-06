using MatchHut.Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MatchHut.Core.Repositories
{
    public interface IRoleClaimRepository : IRepository<RoleClaim>
    {
        new IQueryable<RoleClaim> GetAllDeferred(Expression<Func<RoleClaim, bool>> predicate);

        new IQueryable<RoleClaim> GetAllDeferred();
    }
}