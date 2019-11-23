﻿using Newtonsoft.Json;
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
        public int CategoryID
        {
            get;
            set;
        }
       
        [JsonProperty]
        public int TaxID
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
        //Navigation Property
        public Tax Tax
        {
            get;
            set;
        }
    }
}