using System;
using System.IO;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    internal class ResponseHandler : IResponseHandler
    {
        public ResponseHandler()
        {
        }

        public async Task HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            if (requestOptions == null) throw new ArgumentNullException(nameof(requestOptions));
            if (responseOptions == null) throw new ArgumentNullException(nameof(responseOptions));
            if (response == null) throw new ArgumentNullException(nameof(response));

            string responseText =
                $"Status code: {response.Code} Handled: {response.Handled}\n"+
                $"Content: {response.Content}";

            await File.WriteAllTextAsync(responseOptions.Path,responseText);
        }
    }
}
