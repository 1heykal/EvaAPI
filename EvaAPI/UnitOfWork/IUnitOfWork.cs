using EvaAPI.Entities;
using EvaAPI.Repositories;

namespace EvaAPI.UnitOfWork;

public interface IUnitOfWork
{
    public IRepository<Book> BookRepository { get; }
    public IRepository<Author> AuthorRepository { get; }
    public IRepository<Member> MemberRepository { get; }
    public IRepository<Borrow> BorrowRepository { get; }
    public Task SaveChangesAsync();
}