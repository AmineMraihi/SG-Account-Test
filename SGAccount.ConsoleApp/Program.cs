using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGAccount.Abstraction;
using SGAccount.Data.Repositories;
using SGAccount.DataAccess.Repositories;
using SGAccount.Services.Services;
using System;

namespace SGAccount.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                                .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build())
               .AddScoped<IBankAccountRepository, BankAccountRepository>()
               .AddScoped<ITransactionRepository, TransactionRepository>()
               .AddScoped<IBankAccountService, BankAccountService>()
               .BuildServiceProvider();

            var bankAccountService = serviceProvider.GetService<IBankAccountService>();
            var ammount = bankAccountService.GetAccountValueAtDate(new DateTime(2023, 02, 20));
            Console.WriteLine(ammount);
            
            var mostSpentCategories = bankAccountService.GetMostSpentCategories();
            foreach (var category in mostSpentCategories)
                Console.WriteLine(category);

            Console.ReadKey();
        }
    }
}
