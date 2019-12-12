using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Models.Base
{
    public class StockBase
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
        public int ProductID
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int AvailableQuantity
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int TotalQuantity
        {
            get;
            set;
        }
    }
}
