using Dapper;
using InventoryDBManagement.Models.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.App.FillDB.DBTableHandler
{
    public class DBTableHandler_Vendors : IDBTableHandler
    {

        public override void Fill(IDbConnection connection, int count = 100)
        {
            CreateTable(connection);

            Random random = new Random();

            string InsertionString = GenerateInsertionString();
            for (int i = 0; i < count; ++i)
            {
                VendorDTO vendor = new VendorDTO();
                vendor.CompanyName  = "CompanyName" + (i + 1);
                vendor.Email        = "Email" + (i + 1);
                vendor.MobileNumber = "MNo" + (i + 1);
                vendor.Address      = "Address" + (i + 1);
                vendor.City         = "City" + (i + 1);
                vendor.State        = "State" + (i + 1);

                connection.Execute(InsertionString, vendor);
            }
        }

        protected override string GenerateCreationString()
        {
            string result;
            result = "create table if not exists Vendors(" +
                        "id integer primary key, " +
                        "CompanyName text, " +
                        "Email text, " +
                        "MobileNumber text, " +
                        "Address text, " +
                        "City text, " +
                        "State text);";

            return result;
        }

        protected override string GenerateInsertionString()
        {
            string result;
            result = "insert into Vendors (CompanyName, Email, MobileNumber, Address, City, State)";
            result += " ";
            result += "values (@CompanyName, @Email, @MobileNumber, @Address, @City, @State)";
            return result;
        }
    }
}
