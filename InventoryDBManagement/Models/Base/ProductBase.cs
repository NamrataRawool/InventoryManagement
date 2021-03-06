﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Models.Base
{
    public class ProductBase
    {
        public ProductBase() { }
        public ProductBase(ProductBase rhs)
        {
            CopyFrom(rhs);
        }

        public void CopyFrom(ProductBase rhs)
        {
            ID = rhs.ID;
            Barcode = rhs.Barcode;
            Name = rhs.Name;
            Description = rhs.Description;
            RetailPrice = rhs.RetailPrice;
            WholeSalePrice = rhs.WholeSalePrice;
        }

        [Key]
        [JsonProperty]
        public int ID { get; set; }

        [JsonProperty]
        public string Barcode { get; set; }

        [Required]
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string Description { get; set; }

        [Required]
        [JsonProperty]
        public int RetailPrice { get; set; }

        [Required]
        [JsonProperty]
        public int WholeSalePrice { get; set; }

    }
}
