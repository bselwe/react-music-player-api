using System;
using System.Threading.Tasks;

namespace MusicPlayer.Core
{
    public class CoreUnitOfWork
    {
        private CoreDbContext dbContext;

        public CoreUnitOfWork(CoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task CommitAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
