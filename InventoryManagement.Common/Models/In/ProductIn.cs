using InventoryManagement.Common.Models.Base;
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
        public byte[] Image
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