using Dapper;
using InventoryManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.App.FillDB.DBTableHandler
{
    public class DBTableHandler_Stocks : IDBTableHandler
    {

        public override void Fill(IDbConnection connection, int count = 100)
        {
            CreateTable(connection);

            Random random = new Random();

            string InsertionString = GenerateInsertionString();
            for (int i = 0; i < count; ++i)
            {

                StockDTO stock = new StockDTO();

                stock.ProductID = 1 + random.Next() % 100;
                stock.AvailableQuantity = 1 + random.Next() % 50;
                stock.TotalQuantity = stock.AvailableQuantity + random.Next() % 30;

                connection.Execute(InsertionString, stock);
            }
        }

        protected override string GenerateCreationString()
        {
            string result;
            result =    "create table if not exists Stocks(" +
                        "ID integer primary key," +
                        "ProductID integer, " +
                        "AvailableQuantity integer, " +
                        "TotalQuantity integer);";
            return result;
        }

        protected override string GenerateInsertionString()
        {
            string result;
            result = "insert into Stocks (ProductID, AvailableQuantity, TotalQuantity)";
            result += " ";
            result += "values (@ProductID, @AvailableQuantity, @TotalQuantity)";
            return result;
        }

    }
}
