using Microsoft.EntityFrameworkCore;
using Shared.Initialization;
using System.Threading.Tasks;

namespace Shared.Infrastructure
{
    public class DbMigrationInitializationStage<TContext> : IInitializationStage where TContext: DbContext
    {
        private readonly TContext _dbContext;
        public int Order => 1;

        public DbMigrationInitializationStage(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ExecuteAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }

    }
}
