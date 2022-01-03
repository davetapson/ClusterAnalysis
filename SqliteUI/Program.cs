using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

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

            var ListOfCleanRows = ReadDataFromTextFile(textFile, sql);       // this method fetches data from the text file, cleans it and sends to the SQL lite DB

            var _dataTable = MakeDataTable("Initial Cleaned Data Table", ListOfCleanRows);              // this method takes the cleaned data in makes a data table in c#

            var dataTablePositiveValuesAreOnes = MakePositiveValuesOnes("Data Table with all positives marked as Ones", _dataTable);        // this method takes a data table and makes all the positive values Ones and negatives Zero

            TallyUpThePositives(dataTablePositiveValuesAreOnes);
            Console.WriteLine(" ");

            //ReadAllDataInDB(sql);   // this method fetches all data from the sqlite database data base

            // these find all the positive value in the table in the SQL Lite DB
            //        FindPositiveValues(sql, "Six");
            //        FindPositiveValues(sql, "Eight");
            //         FindPositiveValues(sql, "Ten");
            //        FindPositiveValues(sql, "Twelve");
            //         FindPositiveValues(sql, "Fourteen");

            // this checks an individual cell in the SQL Lite DB to see if it's positive... i might use this to evaluate cells around a positive cell
            //      string row = "8";
            //      string col = "Eight";
            //            var isitpositive = CheckIfPositive(sql, row, col);

            Console.WriteLine(" ");
            Console.WriteLine("Done Processing... ");

            Console.ReadLine();
        }

        private static void TallyUpThePositives(DataTable dataTablePositiveValuesAreOnes)
        {   //a

            Console.WriteLine("Tallying up the positives in Ring 1 and Ring 2... ");
            Console.WriteLine(" ");

            int rowCount = 0;

            //Tally up the positives surrounding the initial values
            foreach (DataRow row1 in dataTablePositiveValuesAreOnes.Rows)                // refer to the table where positives are ones
            {   //c
                    int col = 3; // only the 3rd column is relevant

                    int rwt2 = rowCount - 2;     // top 2
                    int rwt1 = rowCount - 1;     // top 1
                    int rwm = rowCount;           // mid
                    int rwb1 = rowCount + 1;    // bot 1
                    int rwb2 = rowCount + 2;    // bot 2

                    int cL2 = col - 2;           // left 2
                    int cL1= col - 1;           // left 1
                    int cm = col;                // mid
                    int cR1 = col + 1;          // right 1
                    int cR2 = col + 2;          // right 2

                    double center = (double)dataTablePositiveValuesAreOnes.Rows[rwm][cm];           // check the value of the center field

                if ( center > 0 && rowCount >= 2 && rowCount <=7)       // only evaluate if the center field is positive and we are in row 3 to 8
                    {   // e
                        // Row .... Column
                        double ring1_1 = (double)dataTablePositiveValuesAreOnes.Rows[rwt1][cL1];           // one row above ... Left
                        double ring1_2 = (double)dataTablePositiveValuesAreOnes.Rows[rwt1][cm];           // one row above
                        double ring1_3 = (double)dataTablePositiveValuesAreOnes.Rows[rwt1][cR1];          // one row above ... Right

                        double ring1_4 = (double)dataTablePositiveValuesAreOnes.Rows[rwm][cL1];        // left
                        double ring1_5 = (double)dataTablePositiveValuesAreOnes.Rows[rwm][cR1];        // right

                        double ring1_6 = (double)dataTablePositiveValuesAreOnes.Rows[rwb1][cL1];        // one row below .... left
                        double ring1_7 = (double)dataTablePositiveValuesAreOnes.Rows[rwb1][cm];        // one row below
                        double ring1_8 = (double)dataTablePositiveValuesAreOnes.Rows[rwb1][cR1];        // one row below  ... right

                        double sum = ring1_1 + ring1_2 + ring1_3 + ring1_4 + ring1_5 + ring1_6 + ring1_7 + ring1_8;

                        if (sum >= 7)         // Ring 1 has passed result 2
                        {   // f
                            Console.WriteLine($"Result 2 for field : row {rwm + 1}, column {cm}... (this positive value is surrounded by 7 positives in ring 1)");
                            Console.WriteLine("");

                            sum = 0;

                        // tally up ring 2 for result 3
                        // Row .... Column
                            double ring2_1 = (double)dataTablePositiveValuesAreOnes.Rows[rwt2][cL2];           // one row above ... Left 2
                            double ring2_2 = (double)dataTablePositiveValuesAreOnes.Rows[rwt2][cL1];           // one row above ... Left 1
                            double ring2_3 = (double)dataTablePositiveValuesAreOnes.Rows[rwt2][cm];           // one row above
                            double ring2_4 = (double)dataTablePositiveValuesAreOnes.Rows[rwt2][cR1];          // one row above ... Right 1
                            double ring2_5 = (double)dataTablePositiveValuesAreOnes.Rows[rwt2][cR2];          // one row above ... Right 2

                            double ring2_6 = (double)dataTablePositiveValuesAreOnes.Rows[rwt1][cL2];        // left
                            double ring2_7 = (double)dataTablePositiveValuesAreOnes.Rows[rwm][cL2];        // left
                            double ring2_8 = (double)dataTablePositiveValuesAreOnes.Rows[rwb1][cL2];        // left

                            double ring2_9 = (double)dataTablePositiveValuesAreOnes.Rows[rwt1][cR2];        // right
                            double ring2_10 = (double)dataTablePositiveValuesAreOnes.Rows[rwm][cR2];        // right
                            double ring2_11= (double)dataTablePositiveValuesAreOnes.Rows[rwb1][cR2];        // right

                            double ring2_12 = (double)dataTablePositiveValuesAreOnes.Rows[rwb2][cL2];           // one row below ... Left 2
                            double ring2_13 = (double)dataTablePositiveValuesAreOnes.Rows[rwb2][cL1];           // one row below ... Left 1
                            double ring2_14 = (double)dataTablePositiveValuesAreOnes.Rows[rwb2][cm];           // one row below
                            double ring2_15 = (double)dataTablePositiveValuesAreOnes.Rows[rwb2][cR1];          // one row below ... Right 1
                            double ring2_16 = (double)dataTablePositiveValuesAreOnes.Rows[rwb2][cR2];          // one row below ... Right 2

                            double sum2 = ring2_1 + ring2_2 + ring2_3 + ring2_4 + ring2_5 + ring2_6 + ring2_7 + ring2_8 
                                                    + ring2_9 + ring2_10 + ring2_11 + ring2_12 + ring2_13 + ring2_14 + ring2_15 + ring2_16;

                            if (sum2 >= 13)         // Ring 2 has passed for result 3
                            {
                                Console.WriteLine($"Result 3 for field : row {rwm + 1}, column {cm}... (this positive value is surrounded by 13 positives in ring 2)");
                                Console.WriteLine("");

                                sum2 = 0;
                            }
                        }   //f
                    }   //e
                rowCount++;
            } // c
        }   //a

             private static DataTable MakePositiveValuesOnes(string NameOfTable, DataTable _dataTable)
            {
            DataTable dataTableTwo = _dataTable.Copy();

            Console.WriteLine(NameOfTable);

            // Edit the contents of each field based on the value
            foreach (DataRow dataRow in dataTableTwo.Rows)
            {
                for (int i = 1; i <= 5; i++)
                {
                    double value = (double)dataRow[i];
                    if (value > 0)
                    {
                        dataRow[i] = 1;     //change to value One if it's positive
                    }
                    else
                        dataRow[i] = 0;
                }
            }

            //print the contents of the table to the console
            foreach (DataRow dataRow in dataTableTwo.Rows)
            {
                Console.WriteLine($" {dataRow[1]}, {dataRow[2]}, {dataRow[3]}, {dataRow[4]}, {dataRow[5]}  ");
            }

            Console.WriteLine(" ");
            return dataTableTwo;
        }

        private static DataTable MakeDataTable(string NameOfTable, List<string[]> listOfCleanRows)
        {
            DataTable dataTableOne = new DataTable( NameOfTable );
            dataTableOne.Clear();

            // Define and add the columns
            dataTableOne.Columns.Add("Id", typeof(double));
            dataTableOne.Columns.Add("Six", typeof(double));
            dataTableOne.Columns.Add("Eight", typeof(double));
            dataTableOne.Columns.Add("Ten", typeof(double));
            dataTableOne.Columns.Add("Twelve", typeof(double));
            dataTableOne.Columns.Add("Fourteen", typeof(double));

            // Add the rows
            foreach (var row in listOfCleanRows)
            {
                double d0 = Convert.ToDouble(row[0]);
                double d1 = Convert.ToDouble(row[1]);
                double d2 = Convert.ToDouble(row[2]);
                double d3 = Convert.ToDouble(row[3]);
                double d4 = Convert.ToDouble(row[4]);
                double d5 = Convert.ToDouble(row[5]);

                dataTableOne.Rows.Add(new object[] { d0, d1, d2, d3, d4, d5 });
            }

            Console.WriteLine(NameOfTable);

            //print the contents of the table to the console
            foreach (DataRow dataRow in dataTableOne.Rows)
            {
                Console.WriteLine($" {dataRow[1]}, {dataRow[2]}, {dataRow[3]}, {dataRow[4]}, {dataRow[5]}  ");
            }

            Console.WriteLine(" ");
            return dataTableOne;
        }

        private static List<string[]> ReadDataFromTextFile(string textFile, SqliteCrud sql)    // 
        {
            ClearAllDataInDB(sql);      // this clears any previous data in the db

            var rows = db.ReadAllDataFromTextFile(textFile);            //this gets a dirty list of data

            List<string[]> CleanRows = new List<string[]>();        // creates a new list for the cleaned data to go into

            var count = 0;

            foreach (var singleRow in rows)             // this cleans the data
            {
                var CleanRow = singleRow.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (count > 0) // don't capture the first row
                {
                    CleanRows.Add(CleanRow);
                }
                count++;
            }

            foreach (var row in CleanRows)             // this sends the data in each row to the sql lite db
            {
                CreateNewRowInDB(sql, row[0], row[1], row[2], row[3], row[4], row[5]);
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

        private static void ClearAllDataInDB(SqliteCrud sql)     // clears any previous data in the SQL lite DB
        {
            sql.ClearAllData();
        }

        private static void ReadAllDataInDB(SqliteCrud sql)     // reads data in the SQL lite DB
        {
            var rows = sql.GetAllData();

            foreach (var row in rows)
            {
                Console.WriteLine($"{ row.Id }: { row.Six } { row.Eight } { row.Ten } { row.Twelve } { row.Fourteen } ");
            }
        }

        private static void FindPositiveValues(SqliteCrud sql, string col)
        {
            var posValueIds = sql.FindPositiveValuesInColumn(col);
            Console.WriteLine($"Column {col}");
            foreach (var value in posValueIds)
            {
                Console.WriteLine($" Id's: { value.Id } ");
                //   Console.WriteLine($"{ value.Id }: { value.Six } { value.Eight } { value.Ten } { value.Twelve } { value.Fourteen } ");
            }
        }

        private static bool CheckIfPositive(SqliteCrud sql, string _Id, string col)
        {
            var answer = sql.ReturnTrueIfPositive(_Id, col);
            return answer;
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