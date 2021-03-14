using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DepsWebApp.Middlewares
{
    /// <summary>
    /// LoggerMiddleware class.
    /// </summary>
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// InvokeAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation(await GetRequestInfoAsync(context));

            var originalBody = context.Response.Body;

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            _logger.LogInformation(await GetResponseInfoAsync(context));

            await responseBody.CopyToAsync(originalBody);
        }

        private async Task<string> GetRequestInfoAsync(HttpContext context)
        {
            var result = new StringBuilder();
            result.AppendLine("Http Request Info:");
            result.AppendLine($"Request path: {context.Request.Path}");
            result.AppendLine($"Request method: {context.Request.Method}");
            result.AppendLine($"Request body: {await ObtainRequestBody(context.Request)}");
            return result.ToString();
        }

        private async Task<string> GetResponseInfoAsync(HttpContext context)
        {
            var result = new StringBuilder();
            result.AppendLine("Http Response Info:");
            result.AppendLine($"Request status code: {context.Response.StatusCode}");
            result.AppendLine($"Response body: {await ObtainResponseBody(context)}");
            return result.ToString();
        }

        private static async Task<string> ObtainRequestBody(HttpRequest request)
        {
            if (request.Body == null) return string.Empty;
            request.EnableBuffering();
            var encoding = GetEncodingFromContentType(request.ContentType);
            string bodyStr;
            using (var reader = new StreamReader(request.Body, encoding, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync().ConfigureAwait(false);
            }
            request.Body.Seek(0, SeekOrigin.Begin);
            return bodyStr;
        }

        private static async Task<string> ObtainResponseBody(HttpContext context)
        {
            var response = context.Response;
            response.Body.Seek(0, SeekOrigin.Begin);
            var encoding = GetEncodingFromContentType(response.ContentType);
            using (var reader = new StreamReader(response.Body, encoding, detectEncodingFromByteOrderMarks: false, bufferSize: 4096, leaveOpen: true))
            {
                var text = await reader.ReadToEndAsync().ConfigureAwait(false);
                response.Body.Seek(0, SeekOrigin.Begin);
                return text;
            }
        }

        private static Encoding GetEncodingFromContentType(string contentTypeStr)
        {
            if (string.IsNullOrEmpty(contentTypeStr))
            {
                return Encoding.UTF8;
            }
            ContentType contentType;
            try
            {
                contentType = new ContentType(contentTypeStr);
            }
            catch (FormatException)
            {
                return Encoding.UTF8;
            }
            if (string.IsNullOrEmpty(contentType.CharSet))
            {
                return Encoding.UTF8;
            }
            return Encoding.GetEncoding(contentType.CharSet, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
        }
    }
}