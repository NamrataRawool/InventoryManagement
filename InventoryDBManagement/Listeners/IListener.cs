using InventoryDBManagement.Broadcaster;
using InventoryDBManagement.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Listeners
{
    public abstract class IEventListener
    {
        public abstract void OnEvent(IEvent e);

        public abstract void RegisterEvents();

        protected void RegisterEvent(EventType type)
        {
            EventBroadcaster.Get().RegisterListener(type, this);
        }
    }
}
