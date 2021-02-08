using System;
using System.Text.Json.Serialization;

namespace RequestProcessor.App.Models
{
    internal class RequestOptions : IRequestOptions, IResponseOptions
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("method")]
        public string MethodString { get; set; }

        public RequestMethod Method
        {
            get
            {
                return Enum.TryParse(MethodString, true, out RequestMethod method)
                ?
                method
                :
                RequestMethod.Undefined;
            }
        }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Path) &&
                    !string.IsNullOrEmpty(Address) &&
                    !(!string.IsNullOrEmpty(Body) && string.IsNullOrEmpty(ContentType)) &&
                    Method != RequestMethod.Undefined;
            }
        }  
    }
}
