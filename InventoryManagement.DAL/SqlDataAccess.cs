
using InventoryManagement.Common.Models;
using InventoryManagement.DAL.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.DAL
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private DatabaseContext _databaseContext;
        public SqlDataAccess()
        {
            _databaseContext = new DatabaseContext();
        }
        ~SqlDataAccess()
        {
            _databaseContext.Dispose();
        }
        public int AddProduct(Product productIn)
        {
            int success;
            try
            {
                _databaseContext.Products.Add(new Product
                {
                    Name = productIn.Name,
                    NoOfItems = productIn.NoOfItems,
                    RetailPrice = productIn.RetailPrice,
                    WholeSalePrice = productIn.WholeSalePrice,
                    CategoryId = productIn.CategoryId,
                    Description = productIn.Description
                });
                success = _databaseContext.SaveChanges();
                return success;
            }
            catch(Exception ex)
            {
                //implement logger 
                return 0;
            }
          
        }

        public IList<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int productId)
        {
            Product product;
            try
            {
                product = _databaseContext.Products
                   .OrderBy(p => p.Id)
                   .First();
                return product;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
