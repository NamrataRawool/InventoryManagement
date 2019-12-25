using InventoryDBManagement.DAL;
using InventoryManagement.Models.Base;
using InventoryManagement.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Models.Out
{
    public class TransactionOut : TransactionBase
    {
        public TransactionOut(InventoryDBContext context, TransactionDTO dto) : base(dto)
        {
            ProductDetails = new List<ProductOut>();

            // fill ProductDetails
            string[] IDs = dto.ProductIDs.Split(',');
            foreach (string sID in IDs)
            {
                int ID = int.Parse(sID);

                ProductDetails.Add(new ProductOut(context, context.GetProduct(ID)));
            }

            Customer = new CustomerOut(context, context.GetCustomer(dto.CustomerID));
        }

        [JsonProperty]
        public List<ProductOut> ProductDetails { get; set; }

        [JsonProperty]
        public CustomerOut Customer { get; set; }

    }
}
