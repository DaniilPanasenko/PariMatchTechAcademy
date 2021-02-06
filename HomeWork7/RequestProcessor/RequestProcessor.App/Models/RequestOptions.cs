using System;
namespace RequestProcessor.App.Models
{
    internal class RequestOptions : IRequestOptions, IResponseOptions
    {
        private IRequestOptions _requestOptions;

        private IResponseOptions _responseOptions;

        public string Path => _responseOptions.Path;

        public bool IsValid => _requestOptions.IsValid && _responseOptions.IsValid;

        public string Name => _requestOptions.Name;

        public string Address => _requestOptions.Address;

        public string ContentType => _requestOptions.ContentType;

        public string Body => _requestOptions.Body;

        public RequestMethod Method => _requestOptions.Method;

        internal RequestOptions(IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            _requestOptions = requestOptions;
            _responseOptions = responseOptions;
        }

        
    }
}
