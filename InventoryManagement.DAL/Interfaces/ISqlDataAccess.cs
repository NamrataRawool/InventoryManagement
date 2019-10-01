using InventoryManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.DAL.Interfaces
{
    public interface ISqlDataAccess
    {
        void AddProduct(ProductIn productIn);

        ProductOut GetProduct(int productId);

        IList<ProductOut> GetAllProducts();
    }
}
