using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Models.Base
{
    public class CustomerBase
    {

        public CustomerBase() { }

        public CustomerBase(CustomerBase rhs)
        {
            ID = rhs.ID;
            Name = rhs.Name;
            Email = rhs.Email;
            MobileNumber = rhs.MobileNumber;
            PendingAmount = rhs.PendingAmount;
            TotalAmount = rhs.TotalAmount;
        }

        [Key]
        [JsonProperty]
        public int ID { get; set; }

        [Required]
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string MobileNumber { get; set; }

        [JsonProperty]
        public string Email { get; set; }

        [JsonProperty]
        public int TotalAmount { get; set; }

        [JsonProperty]
        public int PendingAmount { get; set; }

    }
}
