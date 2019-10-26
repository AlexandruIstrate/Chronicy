namespace Chronicy.Data
{
    /// <summary>
    /// Provides operations for converting object between two types.
    /// </summary>
    /// <typeparam name="TInput">The type to convert from</typeparam>
    /// <typeparam name="TOutput">The type to convert to</typeparam>
    public interface IConverter<TInput, TOutput>
    {
        bool CanReverseConvert { get; }

        TOutput Convert(TInput value);
        TInput ReverseConvert(TOutput value);
    }
}
