using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryDBManagement.DAL;
using InventoryManagement.Common.Models;
using InventoryManagement.Common.Models.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace InventoryDBManagement.App
{
    public class ServerApp : IApplication
    {

        ~ServerApp()
        {
            m_Context.Dispose();
        }

        public override void Start(string[] args)
        {
            StartWebApp(args);
        }

        private void StartWebApp(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (host)
            {
                Logger = host.Services.GetRequiredService<ILogger<Program>>();
                try
                {
                    m_Context = new InventoryDBContext();
                    m_Context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "An error occurred creating the DB.");
                }

                host.Run();
            }
        }


        private static ILogger Logger
        {
            get;
            set;
        }

        public IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        // data members
        DbContext m_Context;

    }
}
