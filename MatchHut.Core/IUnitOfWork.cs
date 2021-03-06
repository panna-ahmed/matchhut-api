using MatchHut.Core.Repositories;

namespace MatchHut.Core
{
    public interface IUnitOfWork
    {
        IConfigurationRepository ConfigurationRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IRoleClaimRepository RoleClaimRepository { get; }
        ICompanyRepository CompanyRepository { get; }

        bool Save();

        bool TrySave(out string message);

        bool ChangeTracker();

        void ToggleTracking();

        void ToggleTimeout(int value = 500);
    }
}