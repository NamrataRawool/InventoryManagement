using Dapper;
using InventoryManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.App.FillDB.DBTableHandler
{
    public class DBTableHandler_Categories : IDBTableHandler
    {

        public override void Fill(IDbConnection connection, int count = 100)
        {
            CreateTable(connection);

            Random random = new Random();
            string InsertionString = GenerateInsertionString();

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

        protected override string GenerateCreationString()
        {
            string result;
            result =    "create table if not exists Categories(" +
                        "id integer primary key," +
                        "name text, " +
                        "description text, " +
                        "discount integer, " +
                        "cgst real, " +
                        "sgst real);";

            return result;
        }

        protected override string GenerateInsertionString()
        {
            string result;
            result = "insert into Categories (Name, Description, Discount, CGST, SGST)";
            result += " ";
            result += "values (@Name, @Description, @Discount, @CGST, @SGST)";
            return result;
        }

    }
}
