using InventoryManagement.Models.Base;
using InventoryManagement.Models.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Models.DTO
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

    }
}
