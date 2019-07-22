namespace Chronicy.Data.Encoders
{
    public interface IEncoder
    {
        string Encode(string input);
        string Decode(string input);
    }
}
