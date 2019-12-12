using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManagement.Models
{
    public class Tax
    {

        [Key]
        [JsonProperty]
        public int TaxID
        {
            get;
            set;
        }
        [Required]
        [JsonProperty]
        public int CGST
        {
            get;
            set;
        }

        [Required]
        [JsonProperty]
        public int SGST
        {
            get;
            set;
        }

    }
}
