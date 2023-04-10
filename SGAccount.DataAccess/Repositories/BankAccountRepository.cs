using SGAccount.Data.Models;
using SGAccount.Data.Repositories;

namespace SGAccount.DataAccess.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly ITransactionRepository _transactionRepository;

        public BankAccountRepository(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public BankAccount GetBankAccountInfo()
        {
            return new BankAccount
            {
                Montant = 8300,
                Transactions = _transactionRepository.GetTransactionsData()
        };
    }
}
}
