
using InventoryManagement.Common.Models;
using InventoryManagement.DAL.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InventoryManagement.DAL
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private InventoryDBContext _databaseContext;
        public SqlDataAccess()
        {
            _databaseContext = new InventoryDBContext();
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
                var imageDirPath = Path.Combine(Environment.CurrentDirectory, @"Images\");
                if (!Directory.Exists(imageDirPath))
                {
                    Directory.CreateDirectory(imageDirPath);
                }
                if (File.Exists(productIn.ImagePath))
                {
                    //New path images folder
                    var imagePath = Path.Combine(imageDirPath, Path.GetFileName(productIn.ImagePath));
                    if (!File.Exists(imagePath))
                        File.Copy(productIn.ImagePath, imagePath);

                    productIn.ImagePath = Path.GetFileName(productIn.ImagePath);
                }
                _databaseContext.Products.Add(productIn);
                success = _databaseContext.SaveChanges();
                return success;
            }
            catch (Exception)
            {
                //implement logger 
                throw;
            }

        }

        public List<Product> GetAllProducts()
        {
            try
            {
                return _databaseContext.Products.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product GetProduct(int productId)
        {
            Product product;
            try
            {
                product = _databaseContext.Products
                   .Single(p => p.ProductID == productId);
                product.ImagePath = Path.Combine(Environment.CurrentDirectory, @"Images\", product.ImagePath);
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
