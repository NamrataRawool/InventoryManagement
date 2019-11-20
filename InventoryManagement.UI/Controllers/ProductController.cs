
using InventoryManagement.Common.Models;
using InventoryManagement.DAL;
using InventoryManagement.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.UI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        ISqlDataAccess _sqlDataAccess;
        public ProductController(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        [HttpGet]
        [Route("Get({productId})")]
        public Product Get([FromRoute] int productId)
        {
            if (productId <= 0)
                return null;
            Product product = _sqlDataAccess.GetProduct(productId);
            return product;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Product> GetAll()
        {
            return _sqlDataAccess.GetAllProducts();
        }

        [HttpPost]
        [Route("Add")]
        public int AddProduct([FromBody]Product productIn)
        {
            if (productIn == null)
                return 0;

            var productOut = _sqlDataAccess.AddProduct(productIn);
            return productOut;
        }

    }
}
