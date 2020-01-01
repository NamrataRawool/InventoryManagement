using InventoryDBManagement.Events;
using InventoryDBManagement.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Broadcaster
{
    // SINGLETON
    public class EventBroadcaster
    {
        Dictionary<EventType, List<IEventListener>> m_EventListeners;

        private EventBroadcaster()
        {
            m_EventListeners = new Dictionary<EventType, List<IEventListener>>();
        }

        public void RegisterListener(EventType type, IEventListener listener)
        {
            if (m_EventListeners.ContainsKey(type))
            {
                // add to the existing list
                m_EventListeners[type].Add(listener);
                return;
            }

            // create a new List
            m_EventListeners.Add(type, new List<IEventListener>() { listener });
        }

        public void BroadcastEvent(IEvent e)
        {
            EventType type = e.Type();

            if (!m_EventListeners.ContainsKey(type))
            {
                // TODO: raise an Assert
                return;
            }

            var Listeners = m_EventListeners[type];
            foreach (var Listener in Listeners)
            {
                if (Listener != null)
                {
                    Listener.OnEvent(e);
                }
            }

        }

        public static EventBroadcaster Get()
        {
            if (m_Instance == null)
                m_Instance = new EventBroadcaster();

            return m_Instance;
        }

        private static EventBroadcaster m_Instance;

    }
}
