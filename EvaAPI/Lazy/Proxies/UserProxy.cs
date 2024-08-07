using EvaAPI.Entities;
using EvaAPI.Services;

namespace EvaAPI.Lazy.Proxies;

public class UserProxy : ApplicationUser
{
    public override byte[] ProfilePicture
    {
        get
        {
            if (base.ProfilePicture is null)
                base.ProfilePicture ??= ProfilePictureService.GetFor(base.Id);

            return base.ProfilePicture;
        }
    }
}