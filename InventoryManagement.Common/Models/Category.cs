using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Common.Models
{
    public class Category
    {
        [Key]
        [JsonProperty]
        public int Id
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
        public string Description
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public string Discount
        {
            get;
            set;
        }
    }
}
