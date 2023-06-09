﻿using SGAccount.Abstraction;
using SGAccount.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGAccount.Services.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankAccountService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        /// <summary>
        /// returns the value of account at given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public double GetAccountValueAtDate(DateTime date)
        {
            var bankAccount = _bankAccountRepository.GetBankAccountInfo();
            Parallel.ForEach(bankAccount.Transactions, transaction =>
            {
                double ammount = transaction.Ammount;
                if (transaction.Date >= date)
                {
                    ammount *= bankAccount.CurrencyRates[transaction.Currency];
                    bankAccount.Montant -= ammount;
                }
            });

            return bankAccount.Montant;
        }

        //returns the most spent categories
        public IList<Category> GetMostSpentCategories()
        {
            var bankAccount = _bankAccountRepository.GetBankAccountInfo();

            var result = bankAccount.Transactions
                         .GroupBy(x => x.Category)
                         .Where(x => x.Key != Category.Aide
                                     && x.Key != Category.Cadeau
                                     && x.Key != Category.Primes
                                     && x.Key != Category.Salaire)
                         .Select(res => new
                         {
                             Category = res.Key,
                             TotalSpent = res.Sum(x => Math.Abs(x.Ammount))
                         }).OrderByDescending(x => x.TotalSpent);

            return result
                        .Select(x => x.Category)
                        .Take(3)
                        .ToList();
        }
    }
}
