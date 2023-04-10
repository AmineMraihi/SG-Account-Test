using System.Collections.Generic;

namespace SGAccount.Data.Models
{
    public class BankAccount
    {
        public double Montant { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public Dictionary<Currency, double> CurrencyRates { get; set; }
                        = new Dictionary<Currency, double>
                        {
                            {
                                Currency.EUR, 1
                            },
                            {
                                Currency.USD, 1.445
                            },
                            {
                                Currency.JPY, 0.482
                            }
                        };

    }
}
