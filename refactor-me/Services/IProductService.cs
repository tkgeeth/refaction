using refactor_me.Models;
using System;

namespace refactor_me.Services
{
    public interface IProductService
    {
        Product GetProductById(Guid productId);
        Products GetProducts();
        Products SearchByName(string name);
        void  CreateProduct(Product product);
        void UpdateProduct(Guid id, Product product);
        void DeleteProductById(Guid productId);
    }
}
