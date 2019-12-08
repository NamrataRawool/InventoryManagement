using System;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using InventoryManagement.Common.Models.DTO;
using Dapper;

namespace InventoryDBManagement.App
{
    public class FillDBApp : IApplication
    {

        public override void Start(string[] args)
        {
            FillAll();
        }

        private string GenerateInsertString<T>()
        {
            string result = "";
            if (typeof(T) == typeof(ProductDTO))
            {
                result = "insert into Products (Name, Description, RetailPrice, WholesalePrice, ImagePath, CategoryId)";
                result += " ";
                result += "values (@Name, @Description, @RetailPrice, @WholesalePrice, @ImagePath, @CategoryId)";
            }
            else if (typeof(T) == typeof(CategoryDTO))
            {
                result = "insert into Categories (Name, Description, Discount, CGST, SGST)";
                result += " ";
                result += "values (@Name, @Description, @Discount, @CGST, @SGST)";
            }

            return result;
        }

        private void CreateTable<T>(IDbConnection connection)
        {
            string query = "";

            if (typeof(T) == typeof(CategoryDTO))
            {
                query = "create table if not exists Categories(" +
                    "id integer," +
                    "name text, " +
                    "description text, " +
                    "discount integer, " +
                    "cgst real, " +
                    "sgst real);";
            }
            else if (typeof(T) == typeof(ProductDTO))
            {
                query = "create table if not exists Products(" +
                    "id integer," +
                    "name text, " +
                    "description text, " +
                    "retailprice integer, " +
                    "wholesaleprice integer, " +
                    "imagepath text," +
                    "categoryid integer);";
            }

            connection.Open();

            SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        private void FillAll()
        {
            string db_path = "Data Source=./InventoryDb.db;Version=3;";
            using (IDbConnection connection = new SQLiteConnection(db_path))
            {
                FillCategories(connection);
                FillProducts(connection);
            }
        }

        private void FillProducts(IDbConnection connection, int count = 100)
        {
            CreateTable<ProductDTO>(connection);

            Random random = new Random();

            for (int i = 0; i < count; ++i)
            {
                ProductDTO product = new ProductDTO();
                product.ID = i;
                product.Name = "PName" + i;
                product.Description = "PDesc" + i;
                product.RetailPrice = (random.Next() * i) % 1000;
                product.WholeSalePrice = (int)(product.RetailPrice * (0.7 + random.NextDouble()));
                product.ImagePath = "PImagePath" + (random.Next() % 100);
                product.CategoryID = random.Next() % 50;

                connection.Execute(GenerateInsertString<ProductDTO>(), product);
            }
        }

        private void FillCategories(IDbConnection connection, int count = 100)
        {

            CreateTable<CategoryDTO>(connection);

            Random random = new Random();
            for (int i = 0; i < count; ++i)
            {

                CategoryDTO category = new CategoryDTO();
                category.ID = i;
                category.Name = "CName" + i;
                category.Description = "CDesc" + i;
                category.Discount = Math.Abs(random.Next() * i) % 20;
                category.CGST = random.NextDouble();
                category.SGST = random.NextDouble();

                connection.Execute(GenerateInsertString<CategoryDTO>(), category);
            }
        }


    }
}
