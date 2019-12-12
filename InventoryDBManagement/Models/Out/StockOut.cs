using InventoryManagement.Models.Base;
using InventoryManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Models.Out
{
    public class StockOut : StockBase
    {
        public StockOut(StockDTO stockDTO)
        {
            ID = stockDTO.ID;
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
