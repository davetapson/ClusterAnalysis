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
        public List<SingleRowModel> ReadAllRecords(string textFile)
        {
            if (File.Exists(textFile) == false)
            {
                return new List<SingleRowModel>();
            }

            var lines = File.ReadAllLines(textFile);
            List<SingleRowModel> output = new List<SingleRowModel>();

            foreach (var line in lines)
            {
                SingleRowModel c = new SingleRowModel();
                var vals = line.Split(',');

                if (vals.Length < 1)
                {
                    throw new Exception($"Invalid row of data: { line }");
                }

                c.Id = vals[0];
                c.Six = vals[1];
                c.Eight = vals[2];
                c.Ten = vals[3];
                c.Twelve= vals[4];
                c.Fourteen = vals[5];

                output.Add(c);
            }

            return output;
        }

        public void WriteAllRecords(List<SingleRowModel> rows, string textFile)
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