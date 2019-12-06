using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Common.Models.Base
{
    public class CustomerBase
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
        [Required]
        [JsonProperty]
        public string MobileNumber
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public string Email
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int TotalAmount
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int PendingAmount
        {
            get;
            set;
        }

    }
}
