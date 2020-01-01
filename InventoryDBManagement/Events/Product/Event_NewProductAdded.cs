using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Events.Product
{
    class Event_NewProductAdded : IEvent
    {

        public Event_NewProductAdded(int productID)
        {
            m_ProductID = productID;
        }

        public int GetProductID() { return m_ProductID; }

        public override EventType Type()
        {
            return EventType.NewProductAdded;
        }

        private int m_ProductID;
    }
}
