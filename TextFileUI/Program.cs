using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace TextFileUI
{
    class Program
    {
        private static IConfiguration _config;
        private static string textFile;
        private static TextFileDataAccess db = new TextFileDataAccess();

        static void Main(string[] args)
        {
            InitializeConfiguration();
            textFile = _config.GetValue<string>("TextFile");

            GetAllContacts();

            Console.WriteLine(" ");
            Console.WriteLine("Done processing text file, ready to move the cluster data into db for later analysis");

            Console.ReadLine();
        }

        //public static void RemoveUser()
        //{
        //    var contacts = db.ReadAllRecords(textFile);
        //    contacts.RemoveAt(0);
        //    db.WriteAllRecords(contacts, textFile);
        //}

        //public static void RemovePhoneNumberFromUser(string phoneNumber)
        //{
        //    var contacts = db.ReadAllRecords(textFile);
        //    contacts[0].PhoneNumbers.Remove(phoneNumber);
        //    db.WriteAllRecords(contacts, textFile);
        //}

        //private static void UpdateContactsFirstName(string firstName)
        //{
        //    var contacts = db.ReadAllRecords(textFile);
        //    contacts[0].FirstName = firstName;
        //    db.WriteAllRecords(contacts, textFile);
        //}

        private static void GetAllContacts()
        {
            var rows = db.ReadAllRecords(textFile);

            foreach (var singleRow in rows)
            {
                Console.WriteLine($"{ singleRow.Id },{ singleRow.Six},{ singleRow.Eight},{ singleRow.Ten},{ singleRow.Twelve},{ singleRow.Fourteen} ");
            }
        }

        //private static void CreateContact(ContactModel contact)
        //{
        //    var contacts = db.ReadAllRecords(textFile);

        //    contacts.Add(contact);

        //    db.WriteAllRecords(contacts, textFile);
        //}

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }
    }
}