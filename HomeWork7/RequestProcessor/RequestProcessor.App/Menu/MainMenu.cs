using System;
using System.Linq;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Services;

namespace RequestProcessor.App.Menu
{
    /// <summary>
    /// Main menu.
    /// </summary>
    internal class MainMenu : IMainMenu
    {
        private IRequestPerformer _performer;

        private IOptionsSource _options;

        private ILogger _logger;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options">Options source</param>
        /// <param name="performer">Request performer.</param>
        /// <param name="logger">Logger implementation.</param>
        public MainMenu(
            IRequestPerformer performer, 
            IOptionsSource options, 
            ILogger logger)
        {
            _performer = performer;
            _options = options;
            _logger = logger;
        }

        public async Task<int> StartAsync()
        {
            try
            {
                var requestOptions = await _options.GetOptionsAsync();
                PrintUserMessage("Program correctly read options from the file", true);

                var correctRequestOptions = requestOptions
                    .Where(options => options.Item1.IsValid && options.Item2.IsValid);

                foreach (var options in requestOptions.Except(correctRequestOptions))
                {
                    var requestName = options.Item1.Name ?? "Undefined";
                    PrintUserMessage($"{requestName} won't be performed because request is invalid", false);
                    _logger.Log($"MainMenu.StartAsync(): {requestName} request is invalid");
                }
    
                var tasks = correctRequestOptions
                    .Select(options => _performer
                        .PerformRequestAsync(options.Item1, options.Item2))
                    .ToArray();

                await Task.WhenAll(tasks);

                correctRequestOptions
                    .ToList()
                    .ForEach(options =>
                        PrintUserMessage($"{options.Item1.Name ?? "Undefined"} request was performed successfully", true));
            }
            catch (PerformException ex)
            {
                PrintUserMessage($"Unknown exception while performing requests", false);
                _logger.Log(ex, "MainMenu.StartAsync(): PerformException was handled");
                return -1;
            }
            catch (OptionsSourceException ex)
            {
                PrintUserMessage($"Unknown exception while reading options from the file", false);
                _logger.Log(ex, "MainMenu.StartAsync(): OptionsSourceException was handled");
                return -1;
            }

            return 0;
        }

        private void PrintUserMessage(string message, bool isPositive)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (isPositive)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine(message);
        }
    }
}
