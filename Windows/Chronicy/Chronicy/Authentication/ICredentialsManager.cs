namespace Chronicy.Authentication
{
    public interface ICredentialsManager
    {
        AuthenticationState State { get; }

        void Signin(string username, string password);
        void Signout();
    }

    public enum AuthenticationState
    {
        NotSignedIn, SignedIn, SignedOut, OperationPending, Errored
    }
}
