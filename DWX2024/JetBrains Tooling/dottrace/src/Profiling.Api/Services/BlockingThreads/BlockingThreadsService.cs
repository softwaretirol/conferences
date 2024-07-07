namespace Profiling.Api.Services.BlockingThreads
{
    public class BlockingThreadsService: IBlockingThreadsService
    {
        private readonly ILogger<BlockingThreadsService> _logger;
        private readonly object _lock1 = new ();
        private readonly object _lock2 = new ();

        public BlockingThreadsService(ILogger<BlockingThreadsService> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            var task1 = Task.Run(() =>
            {
                lock (_lock1)
                {
                    Thread.Sleep(1000);

                    lock (_lock2)
                    {
                        _logger.LogInformation("Do some work");
                    }
                }
            });

            var task2 = Task.Run(() =>
            {
                lock (_lock2)
                {
                    Thread.Sleep(1000);

                    lock (_lock1)
                    {
                        _logger.LogInformation("Do even more work");
                    }
                }
            });

            Task.WaitAll(task1, task2);
            _logger.LogInformation("Finish work");
        }
    }
}
