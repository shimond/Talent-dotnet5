using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentWebApp.ConfigModel;
using TalentWebApp.Interfaces;
using TalentWebApp.Models;

namespace TalentWebApp.Services
{
    public class MockProductsService : IProductService
    {
        private static List<Product> _list;
        private readonly int productsWaitTime;

        public async Task<List<Product>> GetAllProducts()
        {
            await Task.Delay(this.productsWaitTime);
            return _list;
        }

        public Task<Product> GetById(int id)
        {
            var item = _list.FirstOrDefault(x => x.ID == id);
            return Task.FromResult(item);
        }

        public Task<Product> UpdateProduct(int id, Product p)
        {
            var item = _list.FirstOrDefault(x => x.ID == id);
            if (item == null)
            {
                throw new InvalidOperationException("cannot update Item with id " + id);
            }

            item.ExpirationDate = p.ExpirationDate;
            item.Name = p.Name;
            item.Price = p.Price;
            return Task.FromResult(item);
        }

        public Task DeleteProduct(int id)
        {
            var item = _list.FirstOrDefault(x => x.ID == id);
            if (item == null)
            {
                throw new InvalidOperationException("cannot delete Item with id " + id);
            }
            _list.Remove(item);
            return Task.CompletedTask;
        }

        public Task<Product> AddNewProduct(Product p)
        {
            var lastId = _list.Select(x => x.ID).Last();
            p.ID = lastId + 1;
            _list.Add(p);
            return Task.FromResult(p);
        }

        static MockProductsService()
        {
            _list = new()
            {
                new() { ID = 1, ExpirationDate = DateTime.Now.AddDays(10), Name = "Bamba", Price = 12.6 },
                new() { ID = 2, ExpirationDate = DateTime.Now.AddDays(15), Name = "Dubonim", Price = 11.9 },
                new() { ID = 3, ExpirationDate = DateTime.Now.AddDays(66), Name = "Apropo", Price = 8.8 },
                new() { ID = 4, ExpirationDate = DateTime.Now.AddDays(12), Name = "Doritos", Price = 2.6 },
                new() { ID = 5, ExpirationDate = DateTime.Now.AddDays(17), Name = "Bisli", Price = 1.4 },
                new() { ID = 6, ExpirationDate = DateTime.Now.AddDays(100), Name = "Kefli", Price = 11.3 }

            };
        }

        public MockProductsService(IAppLogger logger, IOptions<DelayConfigModel> delayConfig)
        {
            //this.productsWaitTime = int.Parse(delayConfig["talent:delay:productsWaitTime"]);
            if (delayConfig != null)
            {
                this.productsWaitTime = delayConfig.Value.ProductsWaitTime;
            }

        }
    }
}
