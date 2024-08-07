using EvaLibrary.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EvaAPI.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
   public ApplicationDbContext _context;
    private DbSet<T> _dbSet;
    
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    public virtual async Task<List<T>> GetAllAsync() =>  await _dbSet.ToListAsync();
    public virtual async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);

    public virtual T Get(int id) => _dbSet.Find(id);


}