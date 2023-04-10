using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SGAccount.Abstraction;
using SGAccount.Data.Models;
using SGAccount.Data.Repositories;
using SGAccount.DataAccess.Repositories;
using SGAccount.Services.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using TechTalk.SpecFlow;

namespace SGAccount.Tests.Features
{
    [Binding]
    public class BankAccountValueAtGivenDateSteps
    {
        Mock<ITransactionRepository> mockTransactionRepository;
        Mock<IBankAccountRepository> mockBankAccountRepository;
        IConfigurationRoot configuration;

        public BankAccountValueAtGivenDateSteps()
        {
            mockTransactionRepository = new Mock<ITransactionRepository>();
            mockBankAccountRepository = new Mock<IBankAccountRepository>();
            var builder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();
        }
        [When(@"I check my bank account at a given date '(.*)'")]
        public void WhenICheckMyBankAccountAtAGivenDate(string date)
        {
            var transactions = ScenarioContext.Current.Get<List<Transaction>>("transactions");
            mockTransactionRepository.Setup(x => x.GetTransactionsData()).Returns(transactions);
            IBankAccountRepository bankAccountRepository
                = new BankAccountRepository(mockTransactionRepository.Object, configuration);
            IBankAccountService bankAccountService = new BankAccountService(bankAccountRepository) ;
            var parsedDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var result = bankAccountService.GetAccountValueAtDate(parsedDate);
            ScenarioContext.Current.Set(result, "actualValue");
        }
        
        [Then(@"I should have the result should be '(.*)'")]
        public void ThenIShouldHaveTheResultShouldBe(double expected)
        {
            var actualValue = ScenarioContext.Current.Get<double>("actualValue");
            Assert.AreEqual(expected, actualValue);
        }
    }
}
