using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SGAccount.DataAccess.Repositories;
using System;
using System.IO;
using System.Linq;

namespace SGAccount.Tests
{
    [TestClass]
    public class SGAccountTests
    {
        IConfigurationRoot config;
        private Mock<IConfiguration> mockConfiguration;

        [TestInitialize]
        public void Initialize()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            mockConfiguration = new Mock<IConfiguration>();
        }

        [TestMethod]
        public void FileShouldExist()
        {
            // Arrange
            string filePath = config["FilePath"];

            // Act
            bool fileExists = File.Exists(filePath);

            // Assert
            Assert.IsTrue(fileExists);
        }

        [TestMethod]
        public void GetTransactionsData_FilePathNotConfigured_ThrowsException()
        {
            // Arrange
            mockConfiguration.Setup(x => x["FilePath"]).Returns((string)null);
            var transactionRepository = new TransactionRepository(mockConfiguration.Object);

            // Act and assert
            Assert.ThrowsException<Exception>(() => transactionRepository.GetTransactionsData());
        }

        [TestMethod]
        public void GetTransactionsData_ReturnsExpectedBankAccounts()
        {
            //Arrange
            mockConfiguration.Setup(x => x["FilePath"]).Returns("C:\\Ressources\\account.csv");
            var transactionRepository = new TransactionRepository(mockConfiguration.Object);
            //Act
            var transactions = transactionRepository.GetTransactionsData();
            //Assert
            Assert.IsTrue(transactions.Any());
        }

        [TestMethod]
        public void GetAccountValueAtDate_ReturnValidAmount()
        {
            //Arrange
            mockConfiguration.Setup(x => x["FilePath"]).Returns("C:\\Ressources\\account.csv");
            var transactionRepository = new TransactionRepository(mockConfiguration.Object);

            var bankAccountRepository = new BankAccountRepository(transactionRepository);
            //Act
            //Assert
        }

    }
}
