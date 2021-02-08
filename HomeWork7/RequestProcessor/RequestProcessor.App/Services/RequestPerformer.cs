using System;
using System.Net.Http;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    /// <summary>
    /// Request performer.
    /// </summary>
    internal class RequestPerformer : IRequestPerformer
    {
        private IRequestHandler _requestHandler;

        private IResponseHandler _responseHandler;

        private ILogger _logger;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="requestHandler">Request handler implementation.</param>
        /// <param name="responseHandler">Response handler implementation.</param>
        /// <param name="logger">Logger implementation.</param>
        public RequestPerformer(
            IRequestHandler requestHandler, 
            IResponseHandler responseHandler,
            ILogger logger)
        {
            _requestHandler = requestHandler;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> PerformRequestAsync(
            IRequestOptions requestOptions, 
            IResponseOptions responseOptions)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    IResponse response;
                    try
                    {
                        response = await _requestHandler.HandleRequestAsync(requestOptions);
                        _logger.Log($"RequestPerformer.PerformRequestAsync(): {requestOptions.Name ?? "Undefined"} request was performed successfully");
                    }
                    catch (TaskCanceledException)
                    { 
                        response = new Response(408, "Timeout")
                        {
                            Handled = false
                        };
                        _logger.Log($"RequestPerformer.PerformRequestAsync(): {requestOptions.Name ?? "Undefined"} request has exceeded the time limit");
                    }
                    await _responseHandler.HandleResponseAsync(response, requestOptions, responseOptions);
                    _logger.Log($"RequestPerformer.PerformRequestAsync(): {requestOptions.Name ?? "Undefined"} request was written to the file {responseOptions.Path} successfully");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.Log(ex, "RequestPerformer.PerformRequestAsync(): Unhandled exception");
                    throw new PerformException(ex.Message, ex);
                }
            }
        }
    }
}
