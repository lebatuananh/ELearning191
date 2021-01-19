using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.EF;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private TContext _dbContext;
        private readonly ILogger<UnitOfWork<TContext>> _logger;
        private readonly IUserClaim _userClaim;

        public UnitOfWork(TContext dbContext, ILogger<UnitOfWork<TContext>> logger, IUserClaim userClaim)
        {
            _dbContext = dbContext;
            _logger = logger;
            _userClaim = userClaim;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    OnBeforeCommit();
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Unit of work commit failed");
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void OnBeforeCommit()
        {
            var entityEntries = _dbContext.ChangeTracker.Entries().Where(x =>
                x.Entity is IEntity && (x.State == EntityState.Added || x.State == EntityState.Modified)).ToList();
            foreach (var entry in entityEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            if (entry.Entity is IModifierTrackingEntity)
                            {
                                entry.Property(nameof(IModifierTrackingEntity.CreatedById)).IsModified = true;
                                entry.Property(nameof(IModifierTrackingEntity.CreatedById)).CurrentValue = _userClaim.UserId;
                                entry.Property(nameof(IModifierTrackingEntity.LastUpdatedById)).IsModified = true;
                                entry.Property(nameof(IModifierTrackingEntity.LastUpdatedById)).CurrentValue = _userClaim.UserId;
                            }
                            if (entry.Entity is IDateTracking)
                            {
                                entry.Property(nameof(IDateTracking.CreatedDate)).IsModified = true;
                                entry.Property(nameof(IDateTracking.CreatedDate)).CurrentValue = DateTimeOffset.Now;
                                entry.Property(nameof(IDateTracking.LastUpdatedDate)).IsModified = true;
                                entry.Property(nameof(IDateTracking.LastUpdatedDate)).CurrentValue = DateTimeOffset.Now;
                            }
                            break;
                        }

                    case EntityState.Modified:
                        {
                            if (entry.Entity is ModifierTrackingEntity)
                            {
                                entry.Property(nameof(IModifierTrackingEntity.CreatedById)).IsModified = false;
                                entry.Property(nameof(IModifierTrackingEntity.LastUpdatedById)).IsModified = true;
                                entry.Property(nameof(IModifierTrackingEntity.LastUpdatedById)).CurrentValue = _userClaim.UserId;
                            }
                            if (entry.Entity is IDateTracking)
                            {
                                entry.Property(nameof(IDateTracking.CreatedDate)).IsModified = false;
                                entry.Property(nameof(IDateTracking.LastUpdatedDate)).IsModified = true;
                                entry.Property(nameof(IDateTracking.LastUpdatedDate)).CurrentValue = DateTimeOffset.Now;
                            }
                            break;
                        }

                    case EntityState.Unchanged:
                    case EntityState.Detached:
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (_dbContext == null)
            {
                return;
            }

            _dbContext.Dispose();
            _dbContext = null;
        }
    }
}
