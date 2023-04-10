using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SGAccount.DataAccess.Repositories;
using SGAcount.Common;
using System;
using System.IO;
using System.Linq;

namespace SGAccount.Tests
{
    [TestClass]
    public class SGAccountTests
    {
        IConfigurationRoot configuration;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();
        }

        [TestMethod]
        public void FileShouldExist()
        {
            // Arrange
            var appSettings = configuration.Get<AppSettings>();
            string filePath = appSettings.FilePath;

            // Act
            bool fileExists = File.Exists(Path.Combine(Environment.CurrentDirectory, filePath));

            // Assert
            Assert.IsTrue(fileExists);
        }

        [TestMethod]
        public void GetTransactionsData_ReturnsExpectedBankAccounts()
        {
            //Arrange
            var transactionRepository = new TransactionRepository(configuration);
            //Act
            var transactions = transactionRepository.GetTransactionsData();
            //Assert
            Assert.IsTrue(transactions.Any());
        }
    }
}
