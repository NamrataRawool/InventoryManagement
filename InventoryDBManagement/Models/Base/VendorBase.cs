using InventoryDBManagement.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Models.Base
{
    public class VendorBase
    {

        public VendorBase() { }
        public VendorBase(VendorBase rhs)
        {
            CopyFrom(rhs);
        }
        public void CopyFrom(VendorBase rhs)
        {
            ID = rhs.ID;
            CompanyName = rhs.CompanyName;
            Address = rhs.Address;
            Email = rhs.Email;
            MobileNumber = rhs.MobileNumber;
            City = rhs.City;
            State = rhs.State;
        }
        [Key]
        [JsonProperty]
        public int ID { get; set; }

        [Required]
        [JsonProperty]
        public string CompanyName { get; set; }
        [Required]
        [JsonProperty]
        public string Email { get; set; }
        [Required]
        [JsonProperty]
        public string MobileNumber { get; set; }

        [JsonProperty]
        public string Address { get; set; }

        [JsonProperty]
        public string City { get; set; }

        [JsonProperty]
        public string State { get; set; }

    }

    public class VendorDTO : VendorBase
    {
        public VendorDTO() { }
        public VendorDTO(VendorIn In)
            : base(In)
        {
        }
    }

    public class VendorIn : VendorBase
    {
    }

    public class VendorOut : VendorBase
    {
        public VendorOut(InventoryDBContext context, VendorDTO dto)
            : base(dto)
        {
        }
    }

}
