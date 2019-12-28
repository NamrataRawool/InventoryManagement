﻿using System;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using InventoryManagement.Models.DTO;
using Dapper;
using System.Collections.Generic;
using InventoryDBManagement.App.FillDB;
using InventoryDBManagement.App.FillDB.DBTableHandler;

namespace InventoryDBManagement.App
{
    public class FillDBApp : IApplication
    {

        private Dictionary<FillEntry, IDBTableHandler> m_DBTableHandlers = new Dictionary<FillEntry, IDBTableHandler>();

        enum FillEntry
        {
            Categories      = 1 << 0, // 1
            Products        = 1 << 1, // 2
            Transactions    = 1 << 2, // 4
            Customers       = 1 << 3, // 8
            Stocks          = 1 << 4, // 16

            All = Categories | Products | Transactions | Customers | Stocks,
        }

        public override void Start(string[] args)
        {

            int FillFlag = (int)FillEntry.All;
            //FillFlag |= (int)FillEntry.Categories;
            //FillFlag |= (int)FillEntry.Products;
            //FillFlag |= (int)FillEntry.Customers;
            //FillFlag |= (int)FillEntry.Transactions;
            //FillFlag |= (int)FillEntry.Stocks;

            m_DBTableHandlers.Add(FillEntry.Categories, new DBTableHandler_Categories());
            m_DBTableHandlers.Add(FillEntry.Products, new DBTableHandler_Product());
            m_DBTableHandlers.Add(FillEntry.Customers, new DBTableHandler_Customers());
            m_DBTableHandlers.Add(FillEntry.Transactions, new DBTableHandler_Transactions());
            m_DBTableHandlers.Add(FillEntry.Stocks, new DBTableHandler_Stocks());

            Fill(FillFlag);
        }

        private void Fill(int FillFlag = (int)FillEntry.All)
        {
            string db_path = "Data Source=./InventoryDb.db;Version=3;";
            using (IDbConnection connection = new SQLiteConnection(db_path))
            {
                if((FillFlag & (int)FillEntry.Categories) != 0)
                    m_DBTableHandlers[FillEntry.Categories].Fill(connection);

                if ((FillFlag & (int)FillEntry.Products) != 0)
                    m_DBTableHandlers[FillEntry.Products].Fill(connection);

                if ((FillFlag & (int)FillEntry.Customers) != 0)
                    m_DBTableHandlers[FillEntry.Customers].Fill(connection);

                if ((FillFlag & (int)FillEntry.Transactions) != 0)
                    m_DBTableHandlers[FillEntry.Transactions].Fill(connection);

                if ((FillFlag & (int)FillEntry.Stocks) != 0)
                    m_DBTableHandlers[FillEntry.Stocks].Fill(connection);
            }
        }

    }
}
