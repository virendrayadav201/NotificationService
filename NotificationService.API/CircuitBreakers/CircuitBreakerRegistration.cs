using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http.Resilience;
using NotificationService.API.BackgroundWorkers;
using Polly;
using Polly.RateLimiting;
using Polly.Registry;
using Polly.Timeout;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System;

namespace NotificationService.API.CircuitBreakers
{
    public class CircuitBreakerRegistration
    {
        public static void Register(IServiceCollection services)
        {
            var rateLimiterOptions = new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromSeconds(10),
                QueueLimit = 0,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst
            };

            // Register fallback handler in DI
            services.AddTransient<FallbackHandler>();

            // --- 1. DEFINE RESILIENCE PIPELINE ---
            services.AddResiliencePipeline("Dynamic-pipeline", (pipelineBuilder, context) =>
            {
                pipelineBuilder.AddRetry(new Polly.Retry.RetryStrategyOptions
                {
                    MaxRetryAttempts = 3,
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    Delay = TimeSpan.FromSeconds(2),
                    ShouldHandle = new PredicateBuilder().Handle<HttpRequestException>()
                });

                pipelineBuilder.AddCircuitBreaker(new Polly.CircuitBreaker.CircuitBreakerStrategyOptions
                {
                    MinimumThroughput = 10,
                    FailureRatio = 0.5,
                    SamplingDuration = TimeSpan.FromSeconds(10),
                    BreakDuration = TimeSpan.FromSeconds(10),
                });

                // FIX: Provide a Func<RateLimiterArguments, ValueTask<RateLimitLease>> instead of a FixedWindowRateLimiter instance
                pipelineBuilder.AddRateLimiter(new RateLimiterStrategyOptions
                {
                    RateLimiter = async args =>
                    {
                        var limiter = new FixedWindowRateLimiter(rateLimiterOptions);
                        return await limiter.AcquireAsync(1, args.Context.CancellationToken).ConfigureAwait(false);
                    }
                });

                pipelineBuilder.AddTimeout(new TimeoutStrategyOptions
                {
                    Timeout = TimeSpan.FromSeconds(5),
                });

                // Note: Fallback for both exceptions and non-success responses is implemented below as a DelegatingHandler
            });

            // --- 2. REGISTER HTTP CLIENT ---
            // Notice: We do NOT set BaseAddress here. Use a variable for the IHttpClientBuilder so we can add handlers.
            var clientBuilder = services.AddHttpClient("DynamicClient");

            clientBuilder.AddResilienceHandler("Dynamic-pipeline", (pipelineBuilder, context) =>
            {
                // Attach the pipeline we defined above
                var pipelineProvider = context.ServiceProvider.GetRequiredService<ResiliencePipelineProvider<string>>();
                pipelineBuilder.AddPipeline(pipelineProvider.GetPipeline("Dynamic-pipeline"));
            });

            // Add the fallback handler as a delegating handler so any failures or non-success responses return a fallback
            clientBuilder.AddHttpMessageHandler<FallbackHandler>();

            // --- 3. REGISTER THE WORKER ---
            services.AddHostedService<DynamicWorker>();
        }
    }
}

































































































































































































