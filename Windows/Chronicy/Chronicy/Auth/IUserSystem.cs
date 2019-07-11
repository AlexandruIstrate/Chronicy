namespace Chronicy.Auth
{
    public interface IUserSystem
    {
        User Authenticate(string username, string password);
    }
}
