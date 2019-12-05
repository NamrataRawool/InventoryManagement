using InventoryManagement.Common.Models.Base;
using InventoryManagement.Common.Models.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Models.DTO
{
    public class StockDTO : StockBase
    {
        public StockDTO()
        {

        }
        public StockDTO(StockIn stockIn)
        {
            ProductID = stockIn.ProductID;
            TotalQuantity = stockIn.TotalQuantity;
            AvailableQuantity = stockIn.AvailableQuantity;

        }
        public ProductDTO Product
        {
            get;
            set;
        }
    }
}
