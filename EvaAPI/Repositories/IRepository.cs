namespace EvaAPI.Repositories;

public interface IRepository<T>
{
    public Task<List<T>> GetAllAsync();
    
    public Task<T?> GetByIdAsync(int id);
    
    public Task AddAsync(T entity);
    
    public Task UpdateAsync(T entity);
    
    public Task DeleteAsync(T entity);

}