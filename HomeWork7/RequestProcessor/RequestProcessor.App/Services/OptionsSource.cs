using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    internal class OptionsSource : IOptionsSource
    {
        private string _path;

        public OptionsSource(string path)
        {
            _path = path;
        }

        public async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetOptionsAsync()
        {
            try
            {
                using var reader = File.OpenRead(_path);
                var options = await JsonSerializer
                    .DeserializeAsync<IEnumerable<RequestOptions>>(reader);
                return options.Select(x => ((IRequestOptions)x, (IResponseOptions)x));
            }
            catch (Exception ex)
            {
                throw new OptionsSourceException(ex.Message, ex);
            }
        }
    }
}
