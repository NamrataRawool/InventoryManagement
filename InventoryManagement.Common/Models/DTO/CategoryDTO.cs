using InventoryManagement.Common.Models.Base;
using InventoryManagement.Common.Models.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Models.DTO
{
    public class CategoryDTO : CategoryBase
    {
        public CategoryDTO()
        {

        }

        public CategoryDTO(CategoryIn categoryIn)
        {
            Name = categoryIn.Name;
            Description = categoryIn.Description;
            Discount = categoryIn.Discount;
            Description = categoryIn.Description;
            SGST = categoryIn.SGST;
            CGST = categoryIn.CGST;
        }
    }
}
