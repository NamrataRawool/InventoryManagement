using InventoryManagement.Common.Models.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Common.Models.In
{
    public class ProductIn : ProductBase
    {
        [Required]
        [JsonProperty]
        public int CategoryID
        {
            get;
            set;
        }
        [JsonProperty]
        public List<IFormFile> Images
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public string ImageName
        {
            get;
            set;

        }
    }
}