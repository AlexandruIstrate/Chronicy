namespace Chronicy.Authentication
{
    /// <summary>
    /// Represents a set of operations for managing user authentication.
    /// </summary>
    public interface IAuthenticationManager
    {
        /// <summary>
        /// The state of authentication;
        /// </summary>
        AuthenticationState State { get; }

        /// <summary>
        /// Signs the user in.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        void Signin(string username, string password);
        
        /// <summary>
        /// Signs the user out.
        /// </summary>
        void Signout();
    }

    public enum AuthenticationState
    {
        /// <summary>
        /// The user has not signed in yet.
        /// </summary>
        NotSignedIn,
        /// <summary>
        /// The user is currently signed in.
        /// </summary>
        SignedIn,
        /// <summary>
        /// The user has signed out.
        /// </summary>
        SignedOut,
        /// <summary>
        /// An authentication operation is pending.
        /// </summary>
        OperationPending,
        /// <summary>
        /// The authentication system threw an error.
        /// </summary>
        Errored
    }
}
