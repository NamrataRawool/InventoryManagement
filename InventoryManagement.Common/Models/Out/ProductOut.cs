using InventoryManagement.Common.Models.Base;
using InventoryManagement.Common.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Common.Models.Out
{
    public class ProductOut : ProductBase
    {
        public ProductOut(ProductDTO productDto)
        {
            ID = productDto.ID;
            Name = productDto.Name;
            Description = productDto.Description;
            RetailPrice = productDto.RetailPrice;
            WholeSalePrice = productDto.WholeSalePrice;
            ImagePath = productDto.ImagePath;
            Category = productDto.Category;
        }
        [Required]
        [JsonProperty]
        public string ImagePath
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int Quantity
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public CategoryDTO Category
        {
            get;
            set;
        }
    }
}
