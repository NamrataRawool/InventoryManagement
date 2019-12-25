using InventoryDBManagement.DAL;
using InventoryManagement.Models.Base;
using InventoryManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Models.Out
{
    public class StockOut : StockBase
    {
        public StockOut(InventoryDBContext context, StockDTO stockDTO)
        {
            ID = stockDTO.ID;
            ProductID = stockDTO.ProductID;
            TotalQuantity = stockDTO.TotalQuantity;
            AvailableQuantity = stockDTO.AvailableQuantity;

            ProductDTO dto = context.GetProduct(stockDTO.ProductID);
            Product = new ProductOut(context, dto);
        }

        public ProductOut Product { get; set; }

    }
}
