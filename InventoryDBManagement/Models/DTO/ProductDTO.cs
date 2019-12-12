using InventoryManagement.Models.Base;
using InventoryManagement.Models.In;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Models.DTO
{
    public class ProductDTO : ProductBase
    {
        public ProductDTO() { }

        public ProductDTO(ProductIn productIn) 
            : base(productIn)
        {
            CategoryID = productIn.CategoryID;
        }
      
        [JsonProperty]
        public string ImagePath { get; set; }

        [Required]
        [JsonProperty]
        public int CategoryID { get; set; }

    }
}
