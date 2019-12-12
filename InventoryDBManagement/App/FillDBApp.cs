using System;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using InventoryManagement.Models.DTO;
using Dapper;

namespace InventoryDBManagement.App
{
    public class FillDBApp : IApplication
    {
        public object FileEntry { get; private set; }

        enum FillEntry
        {
            Categories      = 1 << 0,
            Products        = 1 << 1,
            Transactions    = 1 << 2,
            Customers       = 1 << 3,

            All = Categories | Products | Transactions | Customers,
        }

        public override void Start(string[] args)
        {
            int FillFlag = 0;
            FillFlag |= (int)FillEntry.Categories;
            FillFlag |= (int)FillEntry.Products;
            FillFlag |= (int)FillEntry.Customers;

            Fill(FillFlag);
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
            else if (typeof(T) == typeof(CustomerDTO))
            {
                result = "insert into Customers (Name, MobileNumber, Email, PendingAmount, TotalAmount)";
                result += " ";
                result += "values (@Name, @MobileNumber, @Email, @PendingAmount, @TotalAmount)";
            }

            return result;
        }

        private void CreateTable<T>(IDbConnection connection)
        {
            string query = "";

            if (typeof(T) == typeof(CategoryDTO))
            {
                query = "create table if not exists Categories(" +
                    "id integer primary key," +
                    "name text, " +
                    "description text, " +
                    "discount integer, " +
                    "cgst real, " +
                    "sgst real);";
            }
            else if (typeof(T) == typeof(ProductDTO))
            {
                query = "create table if not exists Products(" +
                    "id integer primary key," +
                    "name text, " +
                    "description text, " +
                    "retailprice integer, " +
                    "wholesaleprice integer, " +
                    "imagepath text," +
                    "categoryid integer);";
            }
            else if (typeof(T) == typeof(CustomerDTO))
            {
                query = "create table if not exists Customers(" +
                    "ID integer primary key," +
                    "Name text, " +
                    "Email text, " +
                    "MobileNumber integer, " +
                    "PendingAmount integer, " +
                    "TotalAmount integer);";
            }

            connection.Open();

            SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        private void Fill(int FillFlag = (int)FillEntry.All)
        {
            string db_path = "Data Source=./InventoryDb.db;Version=3;";
            using (IDbConnection connection = new SQLiteConnection(db_path))
            {
                if((FillFlag & (int)FillEntry.Categories) != 0)
                    FillCategories(connection);

                if ((FillFlag & (int)FillEntry.Products) != 0)
                    FillProducts(connection);

                if ((FillFlag & (int)FillEntry.Customers) != 0)
                    FillCustomers(connection);
            }
        }

        private void FillCustomers(IDbConnection connection, int count = 100)
        {
            CreateTable<CustomerDTO>(connection);

            Random random = new Random();

            string InsertionString = GenerateInsertString<CustomerDTO>();
            for (int i = 0; i < count; ++i)
            {

                CustomerDTO customer = new CustomerDTO();
                customer.Name = "PName" + (i + 1);
                customer.MobileNumber = "MNo" + (i + 1);
                customer.Email = "Email" + (i + 1);
                customer.PendingAmount = Math.Abs(random.Next() % 2000);
                customer.TotalAmount = customer.PendingAmount + (random.Next() % 1000);

                connection.Execute(InsertionString, customer);
            }
        }

        private void FillProducts(IDbConnection connection, int count = 100)
        {
            CreateTable<ProductDTO>(connection);

            Random random = new Random();
            string InsertionString = GenerateInsertString<ProductDTO>();

            for (int i = 0; i < count; ++i)
            {
                ProductDTO product = new ProductDTO();
                product.Name = "PName" + (i + 1);
                product.Description = "PDesc" + (i + 1);
                product.RetailPrice = (random.Next() * i) % 1000;
                product.WholeSalePrice = (int)(product.RetailPrice * (0.7 + random.NextDouble()));
                product.ImagePath = "PImagePath" + (random.Next() % 100);
                product.CategoryID = random.Next() % 50 + 1;

                connection.Execute(InsertionString, product);
            }
        }

        private void FillCategories(IDbConnection connection, int count = 100)
        {

            CreateTable<CategoryDTO>(connection);

            Random random = new Random();
            string InsertionString = GenerateInsertString<CategoryDTO>();

            for (int i = 0; i < count; ++i)
            {

                CategoryDTO category = new CategoryDTO();
                category.Name = "CName" + (i + 1);
                category.Description = "CDesc" + (i + 1);
                category.Discount = Math.Abs(random.Next() * i) % 20;
                category.CGST = random.NextDouble();
                category.SGST = random.NextDouble();

                connection.Execute(InsertionString, category);
            }
        }


    }
}
