using InventoryManagement.Common.Models.Out;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InventoryManagement.Common.Models
{
    public class Transaction
    {
        [Key]
        [JsonProperty]
        public int TransactionID
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int TotalPrice
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public string ProductIDs
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public DateTime TransactionDateTime
        {
            get;
            set;
        }
        [NotMapped]
        public List<ProductOut> ProductDetails
        {
            get;
            set;
        }
    }
}
