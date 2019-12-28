using Dapper;
using InventoryManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.App.FillDB.DBTableHandler
{
    public class DBTableHandler_Transactions : IDBTableHandler
    {
        public override void Fill(IDbConnection connection, int count = 100)
        {
            CreateTable(connection);

            Random random = new Random();

            string InsertionString = GenerateInsertionString();
            for (int i = 0; i < count; ++i)
            {

                TransactionDTO transaction = new TransactionDTO();
                transaction.TotalPrice = (int)(1000 * (1 + random.NextDouble()));

                string productids = "";
                int productids_limit = 1 + random.Next() % 10;
                for (int q = 0; q < productids_limit; ++q)
                {
                    int productID = 1 + random.Next() % 100;
                    productids += productID + ",";
                }
                productids = productids.Substring(0, productids.Length - 1);
                transaction.ProductIDs = productids;

                string quantity = "";
                int quantity_limit = productids_limit;
                for (int q = 0; q < quantity_limit; ++q)
                {
                    int temp = 1 + random.Next() % 100;
                    quantity += temp + ",";
                }
                quantity = quantity.Substring(0, quantity.Length - 1);
                transaction.ProductQuantity = quantity;
                transaction.TransactionDateTime = DateTime.Now;
                transaction.CustomerID = 1 + (random.Next() % 100);

                connection.Execute(InsertionString, transaction);
            }
        }

        protected override string GenerateCreationString()
        {
            string result;
            result = "create table if not exists Transactions(" +
                     "ID integer primary key," +
                     "TotalPrice integer, " +
                     "ProductIDs text, " +
                     "ProductQuantity text, " +
                     "TransactionDateTime datetime default current_timestamp, " +
                     "CustomerID integer);";

            return result;
        }

        protected override string GenerateInsertionString()
        {
            string result;
            result = "insert into Transactions (TotalPrice, ProductIDs, ProductQuantity, TransactionDateTime, CustomerID)";
            result += " ";
            result += "values (@TotalPrice, @ProductIDs, @ProductQuantity, @TransactionDateTime, @CustomerID)";
            return result;
        }
    }
}
