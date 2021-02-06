using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    internal class OptionsService : IOptionsSource
    {
        private string _path;

        public OptionsService(string path)
        {
            _path = path;
        }

        public async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetOptionsAsync()
        {
            using FileStream optionsStream = File.Create(_path);
            var options = await JsonSerializer.DeserializeAsync<IEnumerable<(IRequestOptions, IResponseOptions)>> (optionsStream);
            return options;
        }
    }
}
