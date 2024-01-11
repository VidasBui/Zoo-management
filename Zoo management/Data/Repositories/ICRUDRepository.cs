namespace Zoo_management.Data.Repositories
{
    public interface ICRUDRepository<T>
    {
        Task<T?> GetAsync(Guid id);
        Task<T[]> GetManyAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
