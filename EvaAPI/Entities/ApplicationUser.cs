using EvaAPI.Lazy;

namespace EvaAPI.Entities;

public class ApplicationUser
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public IValueHolder<byte[]> ProfilePictureValueHolder { get; set; }

    public byte[] ProfilePicture
    {
        get
        {
            return ProfilePictureValueHolder.GetValue(Id);
        }
        
    }
}
