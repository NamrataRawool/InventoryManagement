using InventoryManagement.Common.Models.Base;
using InventoryManagement.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Models.Out
{
    public class CustomerOut : CustomerBase
    {
        public CustomerOut(CustomerDTO customerDTO)
        {
            CustomerID = customerDTO.CustomerID;
            Name = customerDTO.Name;
            Email = customerDTO.Email;
            MobileNumber = customerDTO.MobileNumber;
            PendingAmount = customerDTO.PendingAmount;
            TotalAmount = customerDTO.TotalAmount;
        }
    }
}
