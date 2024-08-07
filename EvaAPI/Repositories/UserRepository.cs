using EvaAPI.Entities;
using EvaAPI.Lazy;
using EvaAPI.Proxies;
using EvaAPI.Services;
using EvaLibrary.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace EvaAPI.Repositories;

public class UserRepository : GenericRepository<ApplicationUser>
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<List<ApplicationUser>> GetAllAsync()
    {
        
        return (List<ApplicationUser>)(await base.GetAllAsync()).Select(MapToProxy);
    }

    public UserProxy MapToProxy(ApplicationUser user)
    {
        return new UserProxy()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Email = user.Email,
            UserName = user.UserName
        };
    }
}