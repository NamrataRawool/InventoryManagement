using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDBManagement.Configuration.Options
{
    public static class SharedMediaConfigOptions
    {
        public static string Products = "shared\\media\\Products";

        public static string Categories { get; set; }

        public static string Transactions { get; set; }

    }
}
