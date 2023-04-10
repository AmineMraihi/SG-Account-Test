using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Configuration;
using SGAccount.Data.Models;
using SGAccount.Data.Repositories;
using SGAcount.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;


namespace SGAccount.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConfiguration _config;
        private readonly AppSettings appSettings;

        public TransactionRepository(IConfiguration config)
        {
            _config = config;
            appSettings =_config.Get<AppSettings>();
        }
        public virtual IList<Transaction> GetTransactionsData()
        {
            var transactions = new List<Transaction>();
            char separator = ';';
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {

                Delimiter = separator.ToString(),
                Encoding = Encoding.Default,
            };
            TypeConverterOptions t = new TypeConverterOptions
            {
                Formats = new[] { "dd/MM/yyyy" },
                DateTimeStyle = DateTimeStyles.None
            };
            var filePath = appSettings.FilePath;
            if (filePath == null) throw new Exception("File not found");
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, csvConfig, leaveOpen: false))
            {
                csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(t);
                
                //skipping the first rows
                for (int i = 0; i < 4; i++)
                    csv.Read();

                csv.ReadHeader();
                while (csv.Read())
                {
                    var date = csv.GetField<DateTime>("Date");
                    var montant = csv.GetField<double>("Montant");
                    var devise = csv.GetField<Currency>("Devise");
                    var categorie = csv.GetField<Category>("Catégorie");
                    transactions.Add(new Transaction { Date = date, Ammount = montant, Currency = devise, Category = categorie });
                }
            }
            return transactions;
        }
    }
}
