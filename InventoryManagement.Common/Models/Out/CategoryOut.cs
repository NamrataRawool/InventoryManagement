using InventoryManagement.Common.Models.Base;
using InventoryManagement.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Models.Out
{

    public class CategoryOut : CategoryBase
    {
        public CategoryOut(CategoryDTO categoryDTO)
        {
            ID = categoryDTO.ID;
            Name = categoryDTO.Name;
            Description = categoryDTO.Description;
            Discount = categoryDTO.Discount;
            SGST = categoryDTO.SGST;
            CGST = categoryDTO.CGST;
        }
    }
}
