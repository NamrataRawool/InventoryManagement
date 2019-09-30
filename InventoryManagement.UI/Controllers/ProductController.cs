using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.UI.Models;
using Microsoft.AspNetCore.Mvc;
    
namespace InventoryManagement.UI.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        [HttpGet]
        [Route("Get({productId})")]
        public ProductOut Get([FromRoute] string productId)
        {
            ProductOut product = new ProductOut();
            product.Id = productId;
            product.HttpStatus = "Ok";
            return product;
        }

        [HttpGet]
        [Route("GetAll")]
        public ProductOut GetAll()
        {
            ProductOut product = new ProductOut();
            product.HttpStatus = "Ok";
            return product;
        }

        [HttpPost]
        [Route("Add")]
        public ProductOut AddProduct([FromBody]ProductIn productIn)
        {
            ProductOut product = new ProductOut();
            product.HttpStatus = "Ok";
            product.Name = productIn.Name;
            return product;
        }

    }
}
