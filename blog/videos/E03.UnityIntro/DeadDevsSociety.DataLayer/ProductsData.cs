﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using DeadDevsSociety.Framework;

namespace DeadDevsSociety.DataLayer
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
    }

    public interface IProductsData
    {
        IList<ProductEntity> GetAllProducts();
    }

    internal class ProductsData : IProductsData
    {
        private readonly LogService _logService;
        private const string ConnectionString = "Data Source=UnityIntro.sdf";
        private readonly SqlCeConnection _connection = new SqlCeConnection(ConnectionString);

        public ProductsData(LogService logService)
        {
            _logService = logService;
        }

        public IList<ProductEntity> GetAllProducts()
        {
            _logService.WriteLine("data:get");

            IList<ProductEntity> list = new List<ProductEntity>();
            _connection.Open();

            var command = new SqlCeCommand("select * from products", _connection);
            SqlCeDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                list.Add(new ProductEntity
                             {
                                 Id = Convert.ToInt32(reader["Id"]), 
                                 ProductName = reader["ProductName"] as string
                             });
            }

            return list;
        }
    }
}
