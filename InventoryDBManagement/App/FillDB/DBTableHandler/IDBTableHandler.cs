using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.App.FillDB
{
    public abstract class IDBTableHandler
    {
        protected abstract string GenerateInsertionString();
        protected abstract string GenerateCreationString();

        protected void CreateTable(IDbConnection connection)
        {
            string query = GenerateCreationString();

            connection.Open();

            SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public abstract void Fill(IDbConnection connection, int count = 100);
    }
}
