using Dapper;
using InventoryManagement.Models.DTO;
using System;
using System.Data;
using System.Data.SQLite;

namespace InventoryDBManagement.App.FillDB.DBTableHandler
{
    public class DBTableHandler_Product : IDBTableHandler
    {
        public override void Fill(IDbConnection connection, int count = 100)
        {
            CreateTable(connection);

            Random random = new Random();

            string InsertionString = GenerateInsertionString();
            for (int i = 0; i < count; ++i)
            {
                ProductDTO product = new ProductDTO();
                product.Name = "PName" + (i + 1);
                product.Barcode = (1000 + random.Next() % 1000).ToString();
                product.Description = "PDesc" + (i + 1);
                product.RetailPrice = (random.Next() * i) % 1000;
                product.WholeSalePrice = (int)(product.RetailPrice * (0.7 + random.NextDouble()));
                product.ImagePath = "PImagePath" + (random.Next() % 100);
                product.CategoryID = random.Next() % 50 + 1;

                connection.Execute(InsertionString, product);
            }
        }

        protected override string GenerateCreationString()
        {
            string result;
            result =    "create table if not exists Products(" +
                        "id integer primary key, " +
                        "barcode string, " +
                        "name text, " +
                        "description text, " +
                        "retailprice integer, " +
                        "wholesaleprice integer, " +
                        "imagepath text," +
                        "categoryid integer);";

            return result;
        }

        protected override string GenerateInsertionString()
        {
            string result;
            result = "insert into Products (Barcode, Name, Description, RetailPrice, WholesalePrice, ImagePath, CategoryId)";
            result += " ";
            result += "values (@Barcode, @Name, @Description, @RetailPrice, @WholesalePrice, @ImagePath, @CategoryId)";
            return result;
        }

    }
}
