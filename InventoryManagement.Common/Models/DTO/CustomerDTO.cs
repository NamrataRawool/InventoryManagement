using InventoryManagement.Common.Models.Base;
using InventoryManagement.Common.Models.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Models.DTO
{
    public class CustomerDTO : CustomerBase
    {
        public CustomerDTO()
        {

        }
        public CustomerDTO(CustomerIn customerIn)
        {
            Name = customerIn.Name;
            MobileNumber = customerIn.MobileNumber;
            Email = customerIn.Email;
            PendingAmount = customerIn.PendingAmount;
            TotalAmount = customerIn.PendingAmount;
        }
    }
}
