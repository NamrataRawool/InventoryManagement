using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Common.Models
{
    public class Product
    {
        [Key]
        [JsonProperty]
        public int ProductID
        {
            get;
            set;
        }
      
        [JsonProperty]
        public int CategoryID
        {
            get;
            set;
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
        [Required]
        [JsonProperty]
        public int NoOfItems
        {
            get;
            set;
        }
        //Navigation Property
        public Category Category
        {
            get;
            set;
        }

    }
}
