namespace AD09
{
    public interface IInterestingNumbersOptions
    {
        bool IncludePrimes { get; }
        bool IncludeFibonacci { get; }
        bool IncludeSquares { get; }
        bool IncludePerfect { get; }
        bool IncludeTheAnswer { get; }
    }
}
