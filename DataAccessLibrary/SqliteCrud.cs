using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLibrary.Models;
using System.Linq;

namespace DataAccessLibrary
{
    public class SqliteCrud
    {
        private readonly string _connectionString;
        private SqliteDataAccess db = new SqliteDataAccess();

        public SqliteCrud(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<SingleRowModel> GetAllData()
        {
            string sql = "select * from data";
            return db.LoadData<SingleRowModel, dynamic>(sql, new { }, _connectionString);
        }
        public void ClearAllData()
        {
            string sql = "delete from data";
            db.SaveData(sql, new { }, _connectionString);
         //   db.LoadData<SingleRowModel, dynamic>(sql, new { }, _connectionString);
        }

        public void CreateRow(SingleRowModel row)
        {
            // Save the basic row
            string sql = "insert into data (Id, Six, Eight, Ten, Twelve, Fourteen) values (@Id, @Six, @Eight, @Ten, @Twelve, @Fourteen);";
            db.SaveData(sql,
                        new { row.Id, row.Six, row.Eight, row.Ten, row.Twelve, row.Fourteen },
                        _connectionString);
        }
    }
}