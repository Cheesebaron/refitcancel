using Refit;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    [Headers("Accept: application/json")]
    public interface IMyApi
    {
        [Get("/")]
        Task<string> GetUserAsync([Header("ID")]int userId, [Header("U")] string username, [Header("P")] string pass, CancellationToken cancellationToken);
    }

    public class DiagnosticsHandler : DelegatingHandler
    {
        public DiagnosticsHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        { }

        public DiagnosticsHandler()
        { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
#if DEBUG
            var totalElapsedTime = Stopwatch.StartNew();

            Debug.WriteLine($"Request: {request}");
            if(request.Content != null)
            {
                var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Debug.WriteLine($"Request Content: {content}");
            }

            var responseElapsedTime = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken);

            Debug.WriteLine($"Response: {response}");
            if(response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Debug.WriteLine($"Response Content: {content}");
            }

            responseElapsedTime.Stop();
            Debug.WriteLine($"Response elapsed time: {responseElapsedTime.ElapsedMilliseconds} ms");

            totalElapsedTime.Stop();
            Debug.WriteLine($"Total elapsed time: {totalElapsedTime.ElapsedMilliseconds} ms");

            return response;
#else
            return await base.SendAsync(request, cancellationToken);
#endif
        }
    }
}
