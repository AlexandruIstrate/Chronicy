namespace Chronicy.Website.Services
{
    public interface IEmailBuilder
    {
        string Build(string callbackUrl);
    }
}
