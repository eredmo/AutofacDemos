namespace AD10
{
    public class TransientDisposableService : IDisposable
    {
        private readonly ILogger<TransientDisposableService> _logger;

        public TransientDisposableService(ILogger<TransientDisposableService> logger)
        {
            _logger = logger;
            _logger.LogInformation("Creating instance of the TransientDisposableService type...");
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing instance of the TransientDisposableService type...");
        }
    }
}
