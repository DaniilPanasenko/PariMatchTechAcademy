using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RequestProcessor.App.Mapping;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    internal class RequestHandler : IRequestHandler
    {
        private HttpClient _httpClient;

        public RequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResponse> HandleRequestAsync(IRequestOptions requestOptions)
        {
            if (requestOptions == null) throw new ArgumentNullException(nameof(requestOptions));
            if (!requestOptions.IsValid) throw new ArgumentOutOfRangeException(nameof(requestOptions));

            var method = HttpMethodMapper.Map(requestOptions.Method);
            var uri = new Uri(requestOptions.Address);
            using var message = new HttpRequestMessage(method, uri);
            if (requestOptions.ContentType != null)
            {
                message.Content = new StringContent(
                    requestOptions.Body,
                    Encoding.UTF8,
                    requestOptions.ContentType
                );
            }
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(100));

            var responseMessage = await _httpClient.SendAsync(message, cts.Token);

            int code = (int)responseMessage.StatusCode;
            string content = await responseMessage.Content.ReadAsStringAsync();

            var response = new Response(code, content);

            return response;
        }
    }
}
