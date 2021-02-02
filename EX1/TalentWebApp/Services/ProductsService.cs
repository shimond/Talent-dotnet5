using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentWebApp.DataAccess;
using TalentWebApp.DataModels;
using TalentWebApp.Interfaces;
using TalentWebApp.Models;

namespace TalentWebApp.Services
{
    public class ProductsService : IProductService
    {

        private readonly MyAppDataContext _dbContext;

        public ProductsService(MyAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            var result = await _dbContext.Products.ToListAsync();
            return result.Select(productModel=> Map(productModel)).ToList();
        }

        public async Task<Product> GetById(int id)
        {
            var item = await _dbContext.Products.FirstOrDefaultAsync(x => x.ID == id);
            return Map(item);
        }

        public async Task<Product> UpdateProduct(int id, Product p)
        {
            var item = await _dbContext.Products.FirstOrDefaultAsync(x => x.ID == id);
            if (item == null)
            {
                throw new InvalidOperationException("cannot update Item with id "  + id);
            }

            item.ExpirationDate = p.ExpirationDate;
            item.Name = p.Name;
            item.Price = p.Price;
            await _dbContext.SaveChangesAsync();
            return Map(item);
        }

        public async Task DeleteProduct(int id)
        {
            var item = await _dbContext.Products.FirstOrDefaultAsync(x => x.ID == id);
            if (item == null)
            {
                throw new InvalidOperationException("cannot delete Item with id " + id);
            }
            _dbContext.Products.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> AddNewProduct(Product p)
        {
            var productToAdd = Map(p);
            _dbContext.Products.Add(productToAdd);
            await _dbContext.SaveChangesAsync();
            return Map(productToAdd);
        }

        private Product Map(ProductModel model)
        {
            return new Product{
                 ID = model.ID,
                 ExpirationDate = model.ExpirationDate,
                 Name = model.Name,
                 Price = model.Price
            };
        }

        private ProductModel Map(Product dto)
        {
            return new ProductModel
            {
                ID = dto.ID,
                ExpirationDate = dto.ExpirationDate,
                Name = dto.Name,
                Price = dto.Price
            };
        }


    }
}
