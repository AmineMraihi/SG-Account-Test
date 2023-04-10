using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SGAccount.Data.Models;
using SGAccount.Data.Repositories;
using SGAccount.DataAccess.Repositories;
using SGAccount.Services.Services;
using SGAcount.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TechTalk.SpecFlow;

namespace SGAccount.Tests.Features
{
    [Binding]
    public class BankAccountTransactionsSteps
    {
        Mock<ITransactionRepository> mockTransactionRepository;
        IConfigurationRoot configuration;

        public BankAccountTransactionsSteps()
        {
            mockTransactionRepository = new Mock<ITransactionRepository>();
            var builder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();


        }
        [Given(@"the following bank account information:")]
        public void GivenTheFollowingBankAccountInformation(Table table)
        {
            var transactions = table.Rows.Select(row => new Transaction
            {
                Date = DateTime.ParseExact(row["Date"], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Ammount = double.Parse(row["Montant"]),
                Currency = (Currency)Enum.Parse(typeof(Currency), row["Devise"]),
                Category = Enum.Parse<Category>(row["Catégorie"])
            }).ToList();

            ScenarioContext.Current.Set(transactions, "transactions");
        }
        
        [When(@"I get the top three spending categories, certain categories")]
        public void WhenIGetTheTopThreeSpendingCategoriesCertainCategories()
        {
            var transactions = ScenarioContext.Current.Get<List<Transaction>>("transactions");
            mockTransactionRepository.Setup(x => x.GetTransactionsData())
                                   .Returns(transactions);

            var bankAccountRepository = new BankAccountRepository(mockTransactionRepository.Object,
                configuration);
            var bankAccountService = new BankAccountService(bankAccountRepository);
            var topSpentCategories = bankAccountService.GetMostSpentCategories();

            ScenarioContext.Current.Set(topSpentCategories, "topSpentCategories");
        }
        
        [Then(@"the result should be:")]
        public void ThenTheResultShouldBe(Table table)
        {
            var expectedCategories = table.Rows
                    .Select(row => Enum.Parse<Category>(row["Catégorie"]))
                    .ToList();

            var actualCategories = ScenarioContext.Current.Get<List<Category>>("topSpentCategories");
            Assert.IsTrue(expectedCategories.SequenceEqual(actualCategories));
        }
    }
}
