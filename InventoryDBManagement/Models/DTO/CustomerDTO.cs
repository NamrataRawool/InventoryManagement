using InventoryManagement.Models.Base;
using InventoryManagement.Models.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Models.DTO
{
    public class CustomerDTO : CustomerBase
    {
        public CustomerDTO()
        {

        }
        public CustomerDTO(CustomerIn customerIn) : base(customerIn)
        {
        }
    }
}
