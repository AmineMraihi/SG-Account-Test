using System.Collections.Generic;

namespace SGAccount.Data.Models
{
    public class BankAccount
    {
        public double Montant { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public Dictionary<Currency, double> CurrencyRates { get; set; }

    }
}
