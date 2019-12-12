using InventoryDBManagement.DAL;
using InventoryManagement.Models.Base;
using InventoryManagement.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Models.Out
{
    public class ProductOut : ProductBase
    {

        public ProductOut() { }

        public ProductOut(InventoryDBContext context, ProductDTO productDto) 
            : base(productDto)
        {
            ImagePath = productDto.ImagePath;
            Category = new CategoryOut(context, context.GetCategory(productDto.CategoryID));
        }

        [Required]
        [JsonProperty]
        public string ImagePath { get; set; }

        [Required]
        [JsonProperty]
        public int Quantity { get; set; }

        [Required]
        [JsonProperty]
        public CategoryOut Category { get; set; }
    }
}
