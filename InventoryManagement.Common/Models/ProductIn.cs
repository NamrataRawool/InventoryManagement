using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Common.Models
{
    public class ProductIn
    {
        [Required]
        [JsonProperty]
        public string Name
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public string Category
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int Price
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int NoOfItems
        {
            get;
            set;
        }
    }
}
