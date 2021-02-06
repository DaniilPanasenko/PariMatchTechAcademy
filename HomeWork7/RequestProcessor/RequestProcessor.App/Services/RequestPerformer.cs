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
                    RequestOptions options = new RequestOptions(requestOptions, responseOptions);
                    if (options.IsValid)
                    {
                        IResponse response;
                        try
                        {
                            response = await _requestHandler.HandleRequestAsync(requestOptions);
                            _logger.Log("successful request");
                        }
                        catch (TimeoutException ex)
                        {
                            response = new Response(400, "")
                            {
                                Handled = false
                            };
                            _logger.Log(ex, "timeout exception in request");
                        }
                        await _responseHandler.HandleResponseAsync(response, requestOptions, responseOptions);
                        _logger.Log("successful response writed");
                        return true;
                    }
                    _logger.Log("unvalid request options");
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.Log(ex, "unhandled exception");
                    throw new PerformException(ex.Message, ex);
                }
            }
        }
    }
}
