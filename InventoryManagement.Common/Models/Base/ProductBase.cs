using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Common.Models.Base
{
    public class ProductBase
    {
        [Key]
        [JsonProperty]
        public int ID
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

        [JsonProperty]
        public string Description
        {
            get;
            set;
        }

        [Required]
        [JsonProperty]
        public int RetailPrice
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int WholeSalePrice
        {
            get;
            set;
        }
    }
}
