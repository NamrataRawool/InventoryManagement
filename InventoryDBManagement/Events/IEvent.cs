using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Events
{
    public enum EventType
    {

        // Product
        NewProductAdded,

        // Transaction
        UI_Transaction_AddProduct,
    }

    public abstract class IEvent
    {
        public abstract EventType Type();

        public T Cast<T>() 
        {
            return (T)Convert.ChangeType(this, typeof(T));
        }
    }
}
