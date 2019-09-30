using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.UI.Models
{
    public class ProductOut
    {
        [Required]
        [JsonProperty]
        public string Id
        {
            get;
            set;
        }
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
        [Required]
        [JsonProperty]
        public string HttpStatus
        {
            get;
            set;
        }
    }
}
