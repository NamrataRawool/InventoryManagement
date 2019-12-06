using InventoryManagement.Common.Models.Base;
using InventoryManagement.Common.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Models.Out
{
    public class TransactionOut : TransactionBase
    {
        public TransactionOut(TransactionDTO transactionDTO)
        {
            ID = transactionDTO.ID;
            TotalPrice = transactionDTO.TotalPrice;
            ProductIDs = transactionDTO.ProductIDs;
            ProductQuantity = transactionDTO.ProductQuantity;
            TransactionDateTime = transactionDTO.TransactionDateTime;
            CustomerID = transactionDTO.CustomerID;
            Customer = transactionDTO.Customer;
        }
        [JsonProperty]
        public List<ProductOut> ProductDetails
        {
            get;
            set;
        }

        [JsonProperty]
        public CustomerDTO Customer
        {
            get;
            set;
        }
    }
}
