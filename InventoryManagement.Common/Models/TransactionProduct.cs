using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Models
{
    public class TransactionProduct
    {
       public List<Transaction> Transactions
        {
            get;
            set;
        }
        public List<Product> Products
        {
            get;
            set;
        }
    }
}
