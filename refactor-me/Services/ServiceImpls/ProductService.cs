///-----------------------------------------------------------------
///   Namespace:      refactor_me.Services.ServiceImpls
///   Class:          ProductService
///   Description:    CRUD Operations for <c>Product<c>
///   Author:         Geeth Lochana                    Date: 27/09/2017
///-----------------------------------------------------------------

using System;
using System.Linq;
using refactor_me.Models;
using refactor_me.Logger;

namespace refactor_me.Services.ServiceImpls
{
    /// <summary>
    ///   The <c>ProductService</c> type is the implemetation of <c>IProductService</c>
    ///   maipulate the CRUD operation for the Product.
    /// </summary>

    public class ProductService : IProductService
    {
        /// <summary>
        /// The <c>CreateProduct</c> method is used to Create new <c>Product</c>
        /// <para>
        ///     Create new <c>Product</c> according to the details and save it in the database.
        ///     Catch Exception if occur while database operation and log the details.
        /// </para>
        /// </summary>
        /// <param name="model"><c>Product</c> type</param>
        public void CreateProduct(Product model)
        {
            Logging.Info("Entered ProductService:CreateProduct(Product model)");
            if (model != null)
            {
                var product = new Product();
                product.Id = Guid.NewGuid();
                product.DeliveryPrice = model.DeliveryPrice;
                product.Description = model.Description;
                product.Name = model.Name;
                product.Price = model.Price;
                try
                {
                    using (var db = new DatabaseEntities())
                    {
                        db.Products.Add(product);
                        db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    Logging.Error("Database Error", exp);
                }
                Logging.Info("Exit ProductService:CreateProduct(Product model)");
            }
        }

        /// <summary>
        /// The <c>DeleteProductById</c> method is used to delete a existing product by its id <c>Product</c>
        /// <para>
        ///     Delete a existing <c>Product</c> by given Id from the database.
        ///     Catch Exception if occur while database operation and log the details.
        /// </para>
        /// </summary>
        /// <param name="productId">Id of the Product</param>
        public void DeleteProductById(Guid productId)
        {
            Logging.Info("Entered ProductService:DeleteProductById(Guid productId)");
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var product = db.Products.Where(x => x.Id == productId).FirstOrDefault();
                    if (product != null)
                    {
                        db.Products.Remove(product);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception exp)
            {
                Logging.Error("Database Error", exp);
            }
            Logging.Info("Exit ProductService:DeleteProductById(Guid productId)");
        }

        /// <summary>
        /// The <c>GetProductById</c> method is used to view a existing product by its id <c>Product</c>
        /// <para>
        ///     View a <c>Product</c> by given Id from the database.
        ///     Catch Exception if occur while database operation and log the details.
        /// </para>
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>A exiting <c>Product</c> according to request</returns>
        public Product GetProductById(Guid productId)
        {
            Logging.Info("Entered ProductService:GetProductById(Guid productId)");
            Product product = null;
            try
            {
                using (var db = new DatabaseEntities())
                {
                    product = db.Products.Where(x => x.Id == productId).FirstOrDefault();
                    if (product != null)
                    {
                        return new Product
                        {
                            Id = product.Id,
                            DeliveryPrice = product.DeliveryPrice,
                            Price = product.Price,
                            Name = product.Name,
                            Description = product.Description
                        };
                    }
                }
            }
            catch (Exception exp)
            {
                Logging.Error("Database Error", exp);
            }
            Logging.Info("Exit ProductService:GetProductById(Guid productId)");
            return product;
        }

        /// <summary>
        /// The <c>GetProducts</c> method is used to view all existing products in the database
        /// <para>
        ///     View list of <c>Product</c> by given Id from the database.
        ///     Catch Exception if occur while database operation and log the details.
        /// </para>
        /// </summary>
        /// <returns>List of <c>Product</c> s</returns>
        public Products GetProducts()
        {
            Logging.Info("Entered GetProducts()");
            Products products = new Products();
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var list = db.Products.ToList();
                    products.Items = list;
                }
            }
            catch (Exception exp)
            {
                Logging.Error("Database Error", exp);
            }
            Logging.Info("Exit ProductService:GetProducts()");
            return products;
        }
        /// <summary>
        /// The <c>SearchByName</c> method is used to view a existing product by its name
        /// <para>
        ///     View a <c>Product</c> by given name from the database.
        ///     Catch Exception if occur while database operation and log the details.
        /// </para>
        /// </summary>
        /// <param name="name">Name of the Product</param>
        /// <returns>A exiting <c>Product</c> according to request</returns>
        public Products SearchByName(string name)
        {
            Logging.Info("Entered ProductService:SearchByName(string name)");
            Products products = new Products();
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var list = db.Products
                        .Where(x => x.Name == name)
                        .ToList();
                }
            }
            catch (Exception exp)
            {
                Logging.Error("Database Error", exp);
            }
            Logging.Info("Exit ProductService:SearchByName(string name)");
            return products;
        }

        /// <summary>
        /// The <c>UpdateProduct</c> method is used to Update a existing <c>Product</c>
        /// <para>
        ///     Update a existing <c>Product</c> according to the details and save it in the database.
        ///     Catch Exception if occur while database operation and log the details.
        /// </para>
        /// <param name="id">Id of the updating <c>Producy</c></param>
        /// <param name="model">Details of the Updating <c>Product</c></param>
        public void UpdateProduct(Guid id, Product model)
        {
            Logging.Info("Entered ProductService:UpdateProduct(Guid id, Product model)");
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var product = db.Products.Where(x => x.Id == id).FirstOrDefault();
                    if (product != null)
                    {
                        product.DeliveryPrice = model.DeliveryPrice;
                        product.Description = model.Description;
                        product.Name = model.Name;
                        product.Price = model.Price;

                        db.SaveChanges();
                    }
                }
            }
            catch (Exception exp)
            {
                Logging.Error("Database Error", exp);
            }
            Logging.Info("Exit ProductService:UpdateProduct(Guid id, Product model)");
        }
    }
}