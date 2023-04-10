using SGAccount.Data.Models;

namespace SGAccount.Data.Repositories
{
    public interface IBankAccountRepository
    {
        public BankAccount GetBankAccountInfo();
    }
}
