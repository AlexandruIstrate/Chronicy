namespace Chronicy.Authentication
{
    public interface ICredentialsManager
    {
        void Signin(string username, string password);
        void Signout();
    }
}
