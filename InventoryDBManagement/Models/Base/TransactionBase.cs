using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Models.Base
{
    public class TransactionBase
    {

        public TransactionBase() { }

        public TransactionBase(TransactionBase rhs)
        {
            ID = rhs.ID;
            TotalPrice = rhs.TotalPrice;
            ProductIDs = rhs.ProductIDs;
            ProductQuantity = rhs.ProductQuantity;
            TransactionDateTime = rhs.TransactionDateTime;
            CustomerID = rhs.CustomerID;
        }

        [Key]
        [JsonProperty]
        public int ID { get; set; }

        [Required]
        [JsonProperty]
        public int TotalPrice { get; set; }

        [Required]
        [JsonProperty]
        public string ProductIDs { get; set; }

        [Required]
        [JsonProperty]
        public string ProductQuantity { get; set; }

        [Required]
        [JsonProperty]
        public DateTime TransactionDateTime { get; set; }

        [Required]
        [JsonProperty]
        public int CustomerID { get; set; }

    }
}
