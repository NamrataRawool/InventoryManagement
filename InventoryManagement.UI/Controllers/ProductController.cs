
using InventoryManagement.Common.Models;
using InventoryManagement.DAL;
using InventoryManagement.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            Product product = _sqlDataAccess.GetProduct(productId);
            return product;
        }

        [HttpGet]
        [Route("GetAll")]
        public Product GetAll()
        {
            Product product = new Product();
            return product;
        }

        [HttpPost]
        [Route("Add")]
        public int AddProduct([FromBody]Product productIn)
        {
            var productOut = _sqlDataAccess.AddProduct(productIn);
          
            return productOut;
        }

    }
}
