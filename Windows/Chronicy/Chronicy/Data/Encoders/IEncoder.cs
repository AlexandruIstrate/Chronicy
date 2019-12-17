namespace Chronicy.Data.Encoders
{
    /// <summary>
    /// Provides operations for encoding and decoding a string to and from a given format.
    /// </summary>
    public interface IEncoder
    {
        string Encode(string input);
        string Decode(string input);
    }
}
