using InventoryDBManagement.Controllers;
using InventoryDBManagement.DAL;
using InventoryDBManagement.Events;
using InventoryDBManagement.Events.Product;
using InventoryDBManagement.Listeners;
using InventoryManagement.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Handlers
{
    public class StockHandler : IHandler<StockHttpController>
    {
        public StockHandler(InventoryDBContext Context, StockHttpController HttpController) 
            : base(Context, HttpController)
        {
        }

        public override void OnEvent(IEvent e)
        {
            EventType type = e.Type();
        }

        public override void RegisterEvents()
        {
            RegisterEvent(EventType.NewProductAdded);
        }
    }
}
