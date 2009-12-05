using System;
using System.Collections.Generic;
using System.Data;
using FunqUnity.Domain;

namespace FunqUnity.Infrastructure.Repository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
    }

    internal class ProductRepository : IProductRepository
    {
        private readonly Func<string,IDbCommand> _command;

        public ProductRepository(Func<string,IDbCommand> command)
        {
            _command = command;
        }

        public List<Product> GetProducts()
        {
            IDbCommand command = _command("select * from Products");
            command.Connection.Open();            
            IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            var list = new List<Product>();

            while (reader.Read())
            {
                list.Add(new Product() {Name = reader["Name"] as string});

            }

            return list;
        }
    }
}
