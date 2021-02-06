using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestProcessor.App.Models
{
    internal class Response : IResponse
    {
        public bool Handled { get; set; }

        public int Code { get; private set; }

        public string Content { get; private set; }

        public Response(int code, string content)
        {
            Handled = true;
            Code = code;
            Content = content;
        }
    }
}
