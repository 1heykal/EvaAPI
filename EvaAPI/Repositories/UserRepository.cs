using EvaAPI.Entities;
using EvaAPI.Lazy;
using EvaAPI.Services;
using EvaLibrary.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EvaAPI.Repositories;

public class UserRepository : GenericRepository<ApplicationUser>
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<List<ApplicationUser>> GetAllAsync()
    {
        
        return (List<ApplicationUser>)(await base.GetAllAsync()).Select(u =>
        {
            u.ProfilePictureValueHolder = new ValueHolder<byte[]>(parameter =>
            {
                return ProfilePictureService.GetFor(u.Id);
            });

            return u;
        });
    }
}