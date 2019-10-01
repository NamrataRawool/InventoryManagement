using InventoryManagement.Common.Configuration.Options;
using InventoryManagement.Common.Models;
using InventoryManagement.DAL.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace InventoryManagement.DAL
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly ConnectionStringsOptions _conStringOptions;
        public SqlDataAccess(IOptions<ConnectionStringsOptions> conStringOptions)
        {
            _conStringOptions = conStringOptions.Value;
        }
        public void AddProduct(ProductIn productIn)
        {
            throw new NotImplementedException();
        }

        public IList<ProductOut> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public ProductOut GetProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
