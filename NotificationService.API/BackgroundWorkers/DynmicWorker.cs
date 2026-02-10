

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace NotificationService.API.BackgroundWorkers
{
    public class DynamicWorker : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<DynamicWorker> _logger;

        public DynamicWorker(IHttpClientFactory httpClientFactory, ILogger<DynamicWorker> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // 1. Determine the URL dynamically (Logic, DB, or Queue)
                string targetUrl = GetNextUrl();

                try
                {
                    // 2. Create the client
                    // It has NO BaseAddress, but it HAS the Resilience Pipeline attached
                    var client = _httpClientFactory.CreateClient("DynamicClient");

                    _logger.LogInformation("Processing request for: {Url}", targetUrl);

                    // 3. PASS THE FULL URL HERE
                    // The Resilience Pipeline wraps this call automatically.
                    var response = await client.GetAsync(targetUrl, stoppingToken);

                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("Success! {Url} returned {Code}", targetUrl, response.StatusCode);
                    }
                    else
                    {
                        _logger.LogWarning("Failed! {Url} returned {Code}", targetUrl, response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // This catches specific resilience exceptions (like BrokenCircuitException) 
                    // or generic network errors after retries are exhausted.
                    _logger.LogError(ex, "Failed to reach {Url} after retries.", targetUrl);
                }

                // Wait a bit before the next job
                await Task.Delay(2000, stoppingToken);
            }
        }

        // Mock logic to simulate changing URLs
        private string GetNextUrl()
        {
            var seconds = DateTime.Now.Second;

            // Example: Switch URLs based on even/odd seconds
            if (seconds % 2 == 0)
            {
                return "https://jsonplaceholder.typicode.com/todos/1"; // Returns JSON
            }
            else
            {
                return "https://jsonplaceholder.typicode.com/posts/1"; // Returns different JSON
            }
        }
    }
}