using InventoryDBManagement.DAL;
using InventoryManagement.Models.Base;
using InventoryManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Models.Out
{

    public class CategoryOut : CategoryBase
    {
        public CategoryOut(InventoryDBContext context, CategoryDTO categoryDTO) : base(categoryDTO)
        {
        }
    }
}
