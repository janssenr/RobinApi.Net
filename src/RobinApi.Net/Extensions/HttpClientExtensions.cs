using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace RobinApi.Net.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            HttpResponseMessage response = new HttpResponseMessage();
            // In case you want to set a timeout
            //CancellationToken cancellationToken = new CancellationTokenSource(60).Token;

            try
            {
                response = await client.SendAsync(request);
                // If you want to use the timeout you set
                //response = await client.SendRequestAsync(request).AsTask(cancellationToken);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("ERROR: " + e);
            }

            return response;
        }

        public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var method = new HttpMethod("DELETE");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            HttpResponseMessage response = new HttpResponseMessage();
            // In case you want to set a timeout
            //CancellationToken cancellationToken = new CancellationTokenSource(60).Token;

            try
            {
                response = await client.SendAsync(request);
                // If you want to use the timeout you set
                //response = await client.SendRequestAsync(request).AsTask(cancellationToken);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("ERROR: " + e);
            }

            return response;
        }
    }
}
