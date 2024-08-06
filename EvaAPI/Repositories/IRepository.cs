namespace EvaAPI.Repositories;

public interface IRepository<T>
{
    public Task<List<T>> GetAllAsync();
    
    public Task<T?> GetByIdAsync(int id);
    
    public Task AddAsync(T entity);
    
    public void Update(T entity);
    
    public void Delete(T entity);

}