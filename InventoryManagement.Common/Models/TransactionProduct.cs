using System.Collections.Generic;

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
