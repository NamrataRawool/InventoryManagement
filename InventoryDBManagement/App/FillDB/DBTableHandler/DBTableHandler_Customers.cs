using Dapper;
using InventoryManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.App.FillDB.DBTableHandler
{
    public class DBTableHandler_Customers : IDBTableHandler
    {

        public override void Fill(IDbConnection connection, int count = 100)
        {
            CreateTable(connection);

            Random random = new Random();

            string InsertionString = GenerateInsertionString();
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

        protected override string GenerateCreationString()
        {
            string result;
            result =    "create table if not exists Customers(" +
                        "ID integer primary key," +
                        "Name text, " +
                        "Email text, " +
                        "MobileNumber integer, " +
                        "PendingAmount integer, " +
                        "TotalAmount integer);";

            return result;
        }

        protected override string GenerateInsertionString()
        {
            string result;
            result = "insert into Customers (Name, MobileNumber, Email, PendingAmount, TotalAmount)";
            result += " ";
            result += "values (@Name, @MobileNumber, @Email, @PendingAmount, @TotalAmount)";
            return result;
        }

    }
}
