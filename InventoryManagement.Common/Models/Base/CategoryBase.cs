using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Common.Models.Base
{
    public class CategoryBase
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
        public int Discount
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public double CGST
        {
            get;
            set;
        }

        [Required]
        [JsonProperty]
        public double SGST
        {
            get;
            set;
        }
    }
}
