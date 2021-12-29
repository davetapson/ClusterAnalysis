using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace DataAccessLibrary
{
    public class TextFileDataAccess
    {

        //   public List<SingleRowModel> ReadAllData(string textFile)
        public string[] ReadAllDataFromTextFile(string textFile)
        {
            if (File.Exists(textFile) == false)
            {
                throw new Exception($"Could not find text file");
            }

            var lines = File.ReadAllLines(textFile);

            return lines;
        }

        public void WriteAllData(List<SingleRowModel> rows, string textFile)
        {
            List<string> lines = new List<string>();

            foreach (var c in rows)
            {
                lines.Add($"{ c.Id },{ c.Six},{ c.Eight},{ c.Ten},{ c.Twelve},{ c.Fourteen} ");
            }

            File.WriteAllLines(textFile, lines);
        }
    }
}