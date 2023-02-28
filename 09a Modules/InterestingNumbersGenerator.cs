namespace AD09
{
    internal class InterestingNumbersGenerator : IInterestingNumbersGenerator
    {
        private static readonly HashSet<int> Primes = new() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47 };
        private static readonly HashSet<int> Fibonacci = new() { 1, 2, 3, 5, 8, 13, 21, 34 };
        private static readonly HashSet<int> Squares = new() { 1, 4, 9, 16, 25, 36, 49 };
        private static readonly HashSet<int> Perfect = new() { 6, 28 };

        private readonly IInterestingNumbersOptions _options;

        public InterestingNumbersGenerator(IInterestingNumbersOptions options)
        {
            _options = options;
        }

        public IEnumerable<int> GetInterestingNumbers()
        {
            for (int i = 1; i < 50; i++)
            {
                if (_options.IncludePrimes && Primes.Contains(i))
                {
                    yield return i;
                }
                else if (_options.IncludeFibonacci && Fibonacci.Contains(i))
                {
                    yield return i;
                }
                else if (_options.IncludeSquares && Squares.Contains(i))
                {
                    yield return i;
                }
                else if (_options.IncludePerfect && Perfect.Contains(i))
                {
                    yield return i;
                }
                else if (_options.IncludeTheAnswer && i == 42)
                {
                    yield return i;
                }
            }
        }
    }
}