using System;

namespace SGAccount.Data.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public double Ammount { get; set; }
        public Currency Currency { get; set; }
        public Category Category { get; set; }
    }
}
