using Dapper;
using InventoryDBManagement.Models.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.App.FillDB.DBTableHandler
{
    public class DBTableHandler_Purchases : IDBTableHandler
    {

        public override void Fill(IDbConnection connection, int count = 100)
        {
            CreateTable(connection);

            Random random = new Random();

            string InsertionString = GenerateInsertionString();
            for (int i = 0; i < count; ++i)
            {
                PurchaseDTO vendor = new PurchaseDTO();

                // 
                int numProducts = 1 + random.Next() % 19;
                string productIDs = "";
                string productQuantities = "";
                string productBuyingPrices = "";
                double totalBuyingPrice = 0;
                for (int n = 0; n < numProducts; ++n)
                {
                    int id = random.Next() % 99 + 1;
                    int quantity = random.Next() % 20 + 1;
                    double price = random.NextDouble() * 100 + 1;

                    totalBuyingPrice += price * quantity;

                    productIDs += id.ToString() + ",";
                    productQuantities += quantity.ToString() + ",";
                    productBuyingPrices += price.ToString() + ",";
                }
                productIDs = productIDs.Substring(0, productIDs.Length - 1);
                productQuantities = productQuantities.Substring(0, productQuantities.Length - 1);
                productBuyingPrices = productBuyingPrices.Substring(0, productBuyingPrices.Length - 1);

                vendor.VendorID = random.Next() % 99 + 1;
                vendor.ProductIDs = productIDs;
                vendor.ProductQuantities = productQuantities;
                vendor.ProductBuyingPrices = productBuyingPrices;
                vendor.TotalBuyingPrice = totalBuyingPrice;

                connection.Execute(InsertionString, vendor);
            }
        }

        protected override string GenerateCreationString()
        {
            string result;
            result = "create table if not exists Purchases(" +
                        "id integer primary key, " +
                        "VendorID integer, " +
                        "ProductIDs text, " +
                        "ProductQuantities text, " +
                        "ProductBuyingPrices text, " +
                        "TotalBuyingPrice real); ";

            return result;
        }

        protected override string GenerateInsertionString()
        {
            string result;
            result = "insert into Purchases (VendorID, ProductIDs, ProductQuantities, ProductBuyingPrices, TotalBuyingPrice)";
            result += " ";
            result += "values (@VendorID, @ProductIDs, @ProductQuantities, @ProductBuyingPrices, @TotalBuyingPrice)";
            return result;
        }
    }
}
