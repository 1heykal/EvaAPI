using System.Text;

namespace EvaAPI.Services;

public class ProfilePictureService
{
    public static byte[] GetFor(int id)
    {
        return Encoding.Default.GetBytes("some random text");
    }
}