using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace DAL
{
    public abstract class Repo<T>
    {
        
        static string _dir = Directory.GetCurrentDirectory();
        static string _dbStr = _dir.Substring(0, _dir.LastIndexOf("/")) + "/DB.db";
        static string connString = $"Data Source={_dbStr};Version=3;";
        public abstract T GetItemById(int id);
        public abstract T SaveItem(T column);
        public abstract int DeleteItem(int id);
        public abstract T UpdateItem(T item);
        public abstract IEnumerable<T> GetAllItems();

        protected DataTable QueryDB(string sqlString)
        {
            var resTable = new DataTable();
            using (var connection = new SQLiteConnection(connString))
            {
                using (var cmd = new SQLiteCommand(sqlString, connection))
                {
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(resTable);
                    }
                }
            }
            return resTable;
        }
        /*private int dbq(string queryString)
        {
            var
            using (var connection = new SQLiteConnection(connString))
            {
                using (var command = new SQLiteCommand(queryString, connection))
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return id;
        }*/

        /*protected int LastInsertRowId(string sqlString)
        {
            int id = -1;
            using (var connection = new SQLiteConnection(connString))
            {
                string queryString = "SELECT last_insert_rowid()";

                using (var cmd = new SQLiteCommand(queryString, connection))
                {
                    /*command.Connection.Open();
                    id = Convert.ToInt32(command.ExecuteScalar());#1#
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(resTable);
                    }
                }

            }
            return id;
        }*/  
        
        protected int InsertIntoDB(string sqlString)
        {
            int id = 0;
            using (var connection = new SQLiteConnection(connString))
            {
                using (var command = new SQLiteCommand(sqlString, connection))
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                sqlString = "SELECT last_insert_rowid()";

                using (var command = new SQLiteCommand(sqlString, connection))
                {
                    id = Convert.ToInt32(command.ExecuteScalar());
                }

            }

            return id;
        }
    }
}