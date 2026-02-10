using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationService.API.CircuitBreakers
{
    // Simple delegating handler that returns a fallback response when downstream call fails
    public class FallbackHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return CreateFallbackResponse();
                }

                return response;
            }
            catch
            {
                return CreateFallbackResponse();
            }
        }

        private static HttpResponseMessage CreateFallbackResponse()
        {
            var payload = "{ \"message\": \"Fallback response from resilience pipeline\" }";
            return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent(payload, Encoding.UTF8, "application/json")
            };
        }
    }
}
