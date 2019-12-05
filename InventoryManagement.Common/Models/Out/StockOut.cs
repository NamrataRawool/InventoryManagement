using InventoryManagement.Common.Models.Base;
using InventoryManagement.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Common.Models.Out
{
    public class StockOut : StockBase
    {
        public StockOut(StockDTO stockDTO)
        {
            StockID = stockDTO.StockID;
            ProductID = stockDTO.ProductID;
            TotalQuantity = stockDTO.TotalQuantity;
            AvailableQuantity = stockDTO.AvailableQuantity;
            Product = stockDTO.Product;
        }

        public ProductDTO Product
        {
            get;
            set;
        }
    }
}
