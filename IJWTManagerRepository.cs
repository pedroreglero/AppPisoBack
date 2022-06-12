using PisoAppBackend.Models;

namespace PisoAppBackend
{
    public interface IJWTManagerRepository
    {
        Usuario Authenticate(string username, string password, out string token);
    }
}
