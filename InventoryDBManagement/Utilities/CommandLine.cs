using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Utilities
{
    public class CommandLine
    {

        public void Parse(string[] args)
        {
            if (args.Length < 1)
                return;

            foreach (string arg in args)
            {
                if (arg.Contains("action"))
                {
                    string[] splits = arg.Split('=');
                    string key = splits[0];
                    string value = splits[1];
                    m_Data.Add(key, value);
                }
            }
        }

        public string GetAttribute(string key)
        {
            if (!m_Data.ContainsKey(key))
                return null;
            return m_Data[key];
        }

        public static CommandLine Get()
        {
            if (m_Instance == null)
                m_Instance = new CommandLine();

            return m_Instance;
        }

        private CommandLine()
        {
            m_Data = new Dictionary<string, string>();
        }

        // data members
        private static CommandLine m_Instance;
        private Dictionary<string, string> m_Data;


    }
}
