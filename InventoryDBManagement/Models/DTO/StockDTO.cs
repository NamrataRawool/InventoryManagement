using InventoryManagement.Models.Base;
using InventoryManagement.Models.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Models.DTO
{
    public class StockDTO : StockBase
    {
        public StockDTO() { }

        public StockDTO(StockIn stockIn)
        {
            ProductID = stockIn.ProductID;
            TotalQuantity = stockIn.TotalQuantity;
            AvailableQuantity = stockIn.AvailableQuantity;
        }

    }
}
