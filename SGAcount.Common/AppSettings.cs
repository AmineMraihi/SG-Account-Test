using System.Collections.Generic;

namespace SGAcount.Common
{
    public class AppSettings
    {
        public string FilePath { get; set; }
        public double CurrentAccountValue { get; set; }
        public Dictionary<Currency, double> CurrencyRates { get; set; }

    }
}
