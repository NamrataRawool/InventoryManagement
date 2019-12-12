using InventoryManagement.Models.Base;
using InventoryManagement.Models.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Models.DTO
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
