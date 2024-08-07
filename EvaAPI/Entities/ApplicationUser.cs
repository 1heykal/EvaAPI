using EvaAPI.Lazy;

namespace EvaAPI.Entities;

public class ApplicationUser
{
    public virtual int Id { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string Email { get; set; }
    public virtual string UserName { get; set; }
    public virtual string Password { get; set; }
    public virtual byte[] ProfilePicture { get; set; }
    
}
