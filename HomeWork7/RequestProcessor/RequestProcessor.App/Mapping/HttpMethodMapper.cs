using System;
using System.Net.Http;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Mapping
{
    internal static class HttpMethodMapper
    {
        public static HttpMethod Map(RequestMethod requestMethod)
        {
            switch (requestMethod)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                case RequestMethod.Patch:
                    return HttpMethod.Patch;
                case RequestMethod.Undefined:
                default:
                    throw new ArgumentOutOfRangeException(nameof(requestMethod));
            }
        }
    }
}
