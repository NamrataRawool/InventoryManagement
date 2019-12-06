using InventoryManagement.Common.Models.Base;
using InventoryManagement.Common.Models.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Models.DTO
{
    public class TransactionDTO : TransactionBase
    {
        public TransactionDTO()
        {

        }
        public TransactionDTO(TransactionIn transactionIn)
        {
            TotalPrice = transactionIn.TotalPrice;
            ProductIDs = transactionIn.ProductIDs;
            ProductQuantity = transactionIn.ProductQuantity;
            TransactionDateTime = transactionIn.TransactionDateTime;
            CustomerID = transactionIn.CustomerID;
        }

        //Navigation Property
        public CustomerDTO Customer
        {
            get;
            set;
        }
    }
}
