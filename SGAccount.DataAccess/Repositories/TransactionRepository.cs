using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Configuration;
using SGAccount.Data.Models;
using SGAccount.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;


namespace SGAccount.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConfiguration _config;

        public TransactionRepository(IConfiguration config)
        {
            _config = config;
        }
        public IList<Transaction> GetTransactionsData()
        {
            var records = new List<Transaction>();
            char separator = ';';
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {

                Delimiter = separator.ToString(),
                Encoding = Encoding.Default,
                //HeaderValidated = null,
                //MissingFieldFound = null,
                //PrepareHeaderForMatch = args => args.Header.ToLower()
            };
            TypeConverterOptions t = new TypeConverterOptions
            {
                Formats = new[] { "dd/MM/yyyy" },
                DateTimeStyle = DateTimeStyles.None
            };
            var filePath = _config["FilePath"];
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
                    records.Add(new Transaction { Date = date, Ammount = montant, Currency = devise, Category = categorie });
                }
            }
            return records;
        }
    }
}
