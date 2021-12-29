﻿using System;
using System.IO;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace SqliteUI
{
    class Program
    {
        private static IConfiguration _config;      // this is to read the text file
        private static string textFile;     // this is to read the text file
        private static TextFileDataAccess db = new TextFileDataAccess();        // this is to read the text file

        static void Main(string[] args)
        {
            InitializeConfiguration();  // this is to read the text file

            textFile = _config.GetValue<string>("TextFile");        // this is to read the text file

            SqliteCrud sql = new SqliteCrud(GetConnectionString());         // this makes a connection to the sql lite db

            ReadDataFromTextFile(textFile, sql);       // this method fetches data from the text file, cleans it and sends to the SQL lite DB

            //CreateNewRowInDB(sql, "32", "33", "34", "35", "36", "37");            // this adds one line if data to the sql lite db

            ReadAllDataInDB(sql);   // this method fetches data from the sqlite database data base

            Console.WriteLine(" ");
            Console.WriteLine("Done Processing SQLite");

            Console.ReadLine();
        }

        private static List<string[]> ReadDataFromTextFile(string textFile, SqliteCrud sql)    // 
        {
            var rows = db.ReadAllDataFromTextFile(textFile);            //this gets a dirty list of data

            List<string[]> CleanRows = new List<string[]>();        // creates a new list for the cleaned data to go into

            foreach (var singleRow in rows)             // this cleans the data
            {
                var CleanRow = singleRow.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                CleanRows.Add(CleanRow);
            }

            foreach (var row in CleanRows)             // this sends the data in each row to the sql lite db
            {
                CreateNewRowInDB(sql, row[0], row[1], row[2], row[3], row[4], row[5]);

           //     Console.WriteLine($"{ row[0] }, { row[1] }, { row[2] }, { row[3] }, { row[4] }, { row[5] } ");
            }

            return CleanRows;
        }


        private static void CreateNewRowInDB(SqliteCrud sql, string _id, string _six, string _eight, string _ten, string _twelve, string _fourteen)
        // this method sends the data in each row to the sql lite db
        {
            SingleRowModel rowofdata = new SingleRowModel
            {
                Id = _id,
                Six = _six,
                Eight = _eight,
                Ten = _ten,
                Twelve = _twelve,
                Fourteen = _fourteen
            };

            sql.CreateRow(rowofdata);
        }

        private static void ReadAllDataInDB(SqliteCrud sql)     // reads data in the SQL lite DB
        {
            var rows = sql.GetAllData();

            foreach (var row in rows)
            {
                Console.WriteLine($"{ row.Id }: { row.Six } { row.Eight } { row.Ten } { row.Twelve } { row.Fourteen } ");
            }
        }

        private static string GetConnectionString(string connectionStringName = "Default")          // get the connection string for sql lite DB
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }

        private static void InitializeConfiguration()       // this method is from TextFileUI
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _config = builder.Build();
        }
    }
}