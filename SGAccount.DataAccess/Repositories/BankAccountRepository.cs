using Microsoft.Extensions.Configuration;
using SGAccount.Data.Models;
using SGAccount.Data.Repositories;
using SGAcount.Common;

namespace SGAccount.DataAccess.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IConfiguration _config;
        private readonly AppSettings appSettings;

        public BankAccountRepository(ITransactionRepository transactionRepository, IConfiguration config)
        {
            _transactionRepository = transactionRepository;
            _config = config;
            appSettings = _config.Get<AppSettings>();
        }
        public BankAccount GetBankAccountInfo()
        {
            return new BankAccount
            {
                Montant = appSettings.CurrentAccountValue,
                CurrencyRates = appSettings.CurrencyRates,
                Transactions = _transactionRepository.GetTransactionsData()
        };
    }
}
}
