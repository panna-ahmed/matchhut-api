using Microsoft.EntityFrameworkCore;
using MatchHut.Core;
using MatchHut.Core.Repositories;
using MatchHut.Persistence.Repositories;
using System;

namespace MatchHut.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private IConfigurationRepository _configurationRepository;
        private IRoleRepository _roleRepository;
        private IUserRepository _userInfoRepository;
        private IUserRoleRepository _userRoleRepository;
        private IRoleClaimRepository _roleClaimRepository;
        private ICompanyRepository _companyRepository;

        public UnitOfWork(string connectionstring)
        {
            var optionsbuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsbuilder.UseSqlServer(connectionstring);

            _applicationDbContext = new ApplicationDbContext(optionsbuilder.Options, null, null);
        }

        public UnitOfWork(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public IConfigurationRepository ConfigurationRepository => _configurationRepository ??= new ConfigurationRepository(_applicationDbContext);

        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_applicationDbContext);

        public IRoleClaimRepository RoleClaimRepository => _roleClaimRepository ??= new RoleClaimRepository(_applicationDbContext);

        public IUserRepository UserRepository => _userInfoRepository ??= new UserRepository(_applicationDbContext);

        public IUserRoleRepository UserRoleRepository => _userRoleRepository ??= new UserRoleRepository(_applicationDbContext);

        public ICompanyRepository CompanyRepository => _companyRepository ??= new CompanyRepository(_applicationDbContext);

        public bool Save()
        {
            if (!ChangeTracker())
                return true;

            return _applicationDbContext.SaveChanges() > 0;
        }

        public bool TrySave(out string error)
        {
            error = string.Empty;

            try
            {
                if (!ChangeTracker())
                    return true;

                return _applicationDbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool ChangeTracker()
        {
            bool isChanged = false;
            var entries = _applicationDbContext.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.State != EntityState.Unchanged)
                {
                    isChanged = true;
                }
            }

            return isChanged;
        }

        public void ToggleTracking()
        {
            if (_applicationDbContext.ChangeTracker.QueryTrackingBehavior == QueryTrackingBehavior.TrackAll)
            {
                _applicationDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                _applicationDbContext.Database.SetCommandTimeout(10000);
            }
            else
            {
                _applicationDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
                _applicationDbContext.Database.SetCommandTimeout(500);
            }
        }

        public void ToggleTimeout(int value = 500)
        {
            _applicationDbContext.Database.SetCommandTimeout(value);
        }

        public void Dispose() => _applicationDbContext.Dispose();
    }
}