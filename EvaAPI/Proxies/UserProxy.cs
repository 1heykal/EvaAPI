using EvaAPI.Entities;
using EvaAPI.Services;

namespace EvaAPI.Proxies;

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