using System;
using System.IO;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace SqliteUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqliteCrud sql = new SqliteCrud(GetConnectionString());

           CreateNewRow(sql, "14", "26", "27", "28", "29", "30");

            ReadAllData(sql);

            Console.WriteLine("Done Processing SQLite");

            Console.ReadLine();
        }

        private static void CreateNewRow(SqliteCrud sql, string _id, string _six, string _eight, string _ten, string _twelve, string _fourteen)
            // variables passed in are from textfileUI
        {
            SingleRowModel rowofdata = new SingleRowModel
            {
                Id = _id,
                Six = _six,
                Eight = _eight,
                Ten = _ten,
                Twelve = _twelve,
                Fourteen = _fourteen

                    //Id = "12",
                    //Six = "21",
                    //Eight = "22",
                    //Ten = "23",
                    //Twelve = "24",
                    //Fourteen = "25"
            };

            sql.CreateRow(rowofdata);
        }

        private static void ReadAllData(SqliteCrud sql)
        {
            var rows = sql.GetAllData();

            foreach (var row in rows)
            {
                Console.WriteLine($"{ row.Id }: { row.Six } { row.Eight } { row.Ten } { row.Twelve } { row.Fourteen } ");
            }
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

           output = config.GetConnectionString(connectionStringName);

            return output;
        }
    }
}
