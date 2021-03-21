namespace DepsWebApp.Options
{
    /// <summary>
    /// RatesOptions class.
    /// </summary>
    public class RatesOptions
    {
        /// <summary>
        /// Base currency.
        /// </summary>
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Is valid base currency.
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseCurrency);
    }
}
