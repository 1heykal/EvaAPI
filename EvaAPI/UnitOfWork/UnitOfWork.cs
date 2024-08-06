using EvaAPI.Entities;
using EvaAPI.Repositories;
using EvaLibrary.DbContexts;

namespace EvaAPI.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    private GenericRepository<Book>? _bookRepository;
    public IRepository<Book> BookRepository
    {
        get
        {
            _bookRepository ??= new GenericRepository<Book>(_context);
            return _bookRepository;
        }
    }

    private GenericRepository<Author>? _authorRepository;
    public IRepository<Author> AuthorRepository
    {
        get
        {
            _authorRepository ??= new GenericRepository<Author>(_context);
            return _authorRepository;
        }
    }

    private GenericRepository<Member>? _memberRepository;
    public IRepository<Member> MemberRepository
    {
        get
        {
            _memberRepository ??= new GenericRepository<Member>(_context);
            return _memberRepository;
        }
    }

    private GenericRepository<Borrow>? _borrowRepository;
    public IRepository<Borrow> BorrowRepository
    {
        get
        {
            _borrowRepository ??= new GenericRepository<Borrow>(_context);
            return _borrowRepository;
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}