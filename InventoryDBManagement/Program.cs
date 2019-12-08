using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InventoryDBManagement.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InventoryDBManagement.Utilities;
using InventoryDBManagement.App;

namespace InventoryDBManagement
{

    public class Program
    {
        enum Action
        {
            START_WEB_APP,
            FILL_DB
        }

        private Action GetStartAction()
        {
            string action = CommandLine.Get().GetAttribute("action");
            if (action != null && action.Equals("fill_db"))
                return Action.FILL_DB;

            return Action.START_WEB_APP;
        }

        private void ParseCommandLineArgs(string[] args)
        {
            CommandLine.Get().Parse(args);
        }

        private void Start(string[] args)
        {
            ParseCommandLineArgs(args);

            switch (GetStartAction())
            {
                case Action.START_WEB_APP:
                    m_Application = new ServerApp();
                    break;
                case Action.FILL_DB:
                    m_Application = new FillDBApp();
                    break;
            }

            m_Application.Start(args);
        }

        IApplication m_Application;

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Start(args);
        }

    }
}
