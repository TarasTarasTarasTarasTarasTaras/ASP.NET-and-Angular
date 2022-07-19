using Data.Entities;
using Data.Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Data.Data
{
    public class InstagramContextDB : IdentityDbContext<User>
    {
        private readonly ClaimsPrincipal user;

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserLike> UserLikes { get; set; }

        public DbSet<UserSave> UserSaves { get; set; }

        public DbSet<Subscribe> Subscribers { get; set; }

        public string _currentUserExternalId;


        public InstagramContextDB(DbContextOptions<InstagramContextDB> options)
            : base(options)
        {

        }

        public InstagramContextDB(DbContextOptions<InstagramContextDB> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            user = httpContextAccessor.HttpContext?.User;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var userName = user?.Identity?.Name;

            this.ApplyAuditInformation(userName);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            try
            {
                var userName = user?.Identity?.Name;
                this.ApplyAuditInformation(userName);
            }
            catch
            {
                return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInformation(string userName)
        {
            this.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry =>
                {
                    if (entry.Entity is IDeletableEntity deletableEntity)
                    {
                        if (entry.State == EntityState.Deleted)
                        {
                            deletableEntity.DeletedOn = DateTime.UtcNow;
                            deletableEntity.DeletedBy = userName;
                            deletableEntity.IsDeleted = true;

                            entry.State = EntityState.Modified;
                            return;
                        }
                    }

                    if (entry.Entity is IEntity entity)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedOn = DateTime.UtcNow;
                            entity.CreatedBy = userName;
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            entity.ModifiedOn = DateTime.UtcNow;
                            entity.ModifiedBy = userName;
                        }
                    }
                });
        }
    }
}
