namespace Chronicy.Data
{
    public interface IConverter<TInput, TOutput>
    {
        bool CanReverseConvert { get; }

        TOutput Convert(TInput value);
        TInput ReverseConvert(TOutput value);
    }
}
