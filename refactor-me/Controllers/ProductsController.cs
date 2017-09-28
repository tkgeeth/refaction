using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using refactor_me.Services;
using refactor_me.Services.ServiceImpls;
using refactor_me.Logger;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private IProductService productService;
        private IProductOptionService optionService;

        public ProductsController()
        {
            Logging.Info("Enterted ProductsController Constructor");
            Logging.Info("Initializing ProductService");
            productService = new ProductService();
            Logging.Info("Initialized ProductService");
            Logging.Info("Initializing ProductOptionService");
            optionService = new ProductOptionService();
            Logging.Info("Initialized ProductOptionService");
        }

        #region Product
        [Route]
        [HttpGet]
        public Products GetAll()
        {
            Logging.Info("Calling GET /products");
            return productService.GetProducts();
        }

        [Route]
        [HttpGet]
        public Products SearchByName(string name)
        {
            Logging.Info("Calling GET /products?name={name}");
            return productService.SearchByName(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            Logging.Info("Calling GET /products/{id}");
            var product = productService.GetProductById(id);
            if (product == null)
            {
                Logging.Info("Not found : Id=" + id);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            Logging.Info("Calling POST /products");
            productService.CreateProduct(product);
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            Logging.Info("Calling PUT / products /{ id}");           
            productService.UpdateProduct(id, product);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            Logging.Info("Calling DELETE /products/{id}");
            productService.DeleteProductById(id);
        }
        #endregion

        #region ProductOption
        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            Logging.Info("Calling GET /products/{id}/options");
            return optionService.GetOptions(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid optionId)
        {
            Logging.Info("Calling GET /products/{id}/options/{optionId}");
            var option = optionService.GetOption(productId, optionId);
            if (option == null)
            {
                Logging.Info("Not found : productId=" + productId + " optionId=" + optionId);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            Logging.Info("Calling POST /products/{id}/options");
            optionService.CreateOption(productId, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            Logging.Info("Calling PUT /products/{id}/options/{optionId}");
            optionService.UpdateOption(id, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            Logging.Info("Calling DELETE /products/{id}/options/{optionId}");
            optionService.DeleteProduct(id);
        }
        #endregion
    }
}
