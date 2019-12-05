using InventoryManagement.Common.Models.Base;
using InventoryManagement.Common.Models.In;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Common.Models.DTO
{
    public class ProductDTO : ProductBase
    {
        public ProductDTO()
        {

        }
        public ProductDTO(ProductIn productIn)
        {
            Name = productIn.Name;
            Description = productIn.Description;
            RetailPrice = productIn.RetailPrice;
            WholeSalePrice = productIn.WholeSalePrice;
            CategoryID = productIn.CategoryID;
        }
      
        [JsonProperty]
        public string ImagePath
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int CategoryID
        {
            get;
            set;
        }

        //Navigation Property
        public CategoryDTO Category
        {
            get;
            set;
        }
    }
}
