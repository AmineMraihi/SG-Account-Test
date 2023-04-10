using SGAccount.Data.Models;
using System.Collections.Generic;

namespace SGAccount.Data.Repositories
{
    public interface ITransactionRepository
    {
        public IList<Transaction> GetTransactionsData();

    }
}
