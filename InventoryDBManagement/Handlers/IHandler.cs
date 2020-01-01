using InventoryDBManagement.DAL;
using InventoryDBManagement.Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Handlers
{
    public abstract class IHandler : IEventListener
    {
        protected InventoryDBContext m_Context;

        public IHandler(InventoryDBContext Context)
        {
            m_Context = Context;

            RegisterEvents();
        }
    }

    public abstract class IHandler<HTTP_CONTROLLER> : IHandler
    {
        protected HTTP_CONTROLLER m_HttpController;

        public IHandler(InventoryDBContext Context, HTTP_CONTROLLER HttpController)
            : base(Context)
        {
            m_HttpController = HttpController;
        }

    }
}
