using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentWebApp.Models;

namespace TalentWebApp.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();

        Task<Product> GetById(int id);

        Task<Product> UpdateProduct(int id, Product p);

        Task DeleteProduct(int id);

        Task<Product> AddNewProduct(Product p);

    }
}
