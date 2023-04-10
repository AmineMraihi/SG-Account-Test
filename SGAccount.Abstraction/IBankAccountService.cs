using System;
using System.Collections.Generic;

namespace SGAccount.Abstraction
{
    public interface IBankAccountService
    {
        public double GetAccountValueAtDate(DateTime date);
        public IList<Category> GetMostSpentCategories();
    }
}
