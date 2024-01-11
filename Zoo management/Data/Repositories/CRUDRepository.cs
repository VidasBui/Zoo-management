using Microsoft.EntityFrameworkCore;

namespace Zoo_management.Data.Repositories
{
    public class CRUDRepository<T> : ICRUDRepository<T> where T: class
    {
        protected readonly ZooDbContext zooDbContext;
        protected readonly DbSet<T> dbSet;
        protected readonly Func<T, Guid> getId;

        public CRUDRepository(ZooDbContext dbContext, Func<ZooDbContext, DbSet<T>> getDbSet, Func<T, Guid> getId)
        {
            zooDbContext = dbContext;
            dbSet = getDbSet(dbContext);
            this.getId = getId;
        }

        public async virtual Task<T?> GetAsync(Guid id) 
        {
            var list = await dbSet.ToListAsync();
            return list.FirstOrDefault(x => getId(x).Equals(id));
        }

        public async virtual Task<T[]> GetManyAsync()
        {
            return await dbSet.ToArrayAsync();
        }

        public async virtual Task CreateAsync(T entity)
        {
            dbSet.Add(entity);
            await zooDbContext.SaveChangesAsync();
        }

        public async virtual Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await zooDbContext.SaveChangesAsync();
        }

        public async virtual Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await zooDbContext.SaveChangesAsync();
        }
    }
}
