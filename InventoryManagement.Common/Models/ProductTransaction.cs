using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Models
{
    public class ProductTransaction
    {
        public int ProductID
        {
            get;
            set;
        }
        public Product Product
        {
            get;
            set;
        }

        public int TransactionID
        {
            get;
            set;
        }
        public Transaction Transaction
        {
            get;
            set;
        }
    }
}
