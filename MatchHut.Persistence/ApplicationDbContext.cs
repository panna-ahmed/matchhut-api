using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MatchHut.Core;
using MatchHut.Core.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MatchHut.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IHttpContextAccessor _context;
        private readonly ApplicationData _applicationData;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor context, ApplicationData applicationData)
            : base(options)
        {
            _context = context;
            _applicationData = applicationData;
        }

        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }     
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer(optionsBuilder., options => options.EnableRetryOnFailure());
            //}
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

            string username = string.Empty;
            if (_context?.HttpContext != null
                && (!_applicationData?.IsJobRunning).Value)
                username = _context.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value??"Anonymous";
            else
                username = "JobUser";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).CreatedDate = DateTime.Now;
                    ((EntityBase)entity.Entity).CreatedBy = username;

                    ((EntityBase)entity.Entity).Status = Statuses.Active.Humanize();
                }

                ((EntityBase)entity.Entity).ModifiedDate = DateTime.Now;
                ((EntityBase)entity.Entity).ModifiedBy = username;
            }
        }
    }
}