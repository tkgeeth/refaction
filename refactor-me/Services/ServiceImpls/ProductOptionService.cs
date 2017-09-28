using System;
using System.Linq;
using refactor_me.Models;
using refactor_me.Logger;

namespace refactor_me.Services.ServiceImpls
{
    public class ProductOptionService : IProductOptionService
    {
        public void CreateOption(Guid productId, ProductOption option)
        {
            Logging.Info("Entered ProductOptionService:CreateOption(Guid productId, ProductOption option)");
            try
            {
                using (var db = new DatabaseEntities())
                {
                    option.ProductId = productId;
                    option.Id = Guid.NewGuid();
                    db.ProductOptions.Add(option);
                    db.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                Logging.Error("Database Error", exp);
            }
            Logging.Info("Exit ProductOptionService:CreateOption(Guid productId, ProductOption option)");
        }

        public void DeleteProduct(Guid optionId)
        {
            Logging.Info("Entered ProductOptionService:DeleteProduct(Guid optionId)");
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var option = db.ProductOptions.Where(x => x.Id == optionId).FirstOrDefault();
                    if (option != null)
                    {
                        db.ProductOptions.Remove(option);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception exp)
            {
                Logging.Error("Database Error", exp);
            }
            Logging.Info("Exit ProductOptionService:DeleteProduct(Guid optionId)");
        }

        public ProductOption GetOption(Guid productId, Guid optionId)
        {
            Logging.Info("Entered ProductOptionService:GetOption(Guid productId, Guid optionId)");

            ProductOption option = null;

            try
            {
                using (var db = new DatabaseEntities())
                {
                    option = db.ProductOptions.Where(x => x.Id == optionId && x.ProductId == productId).FirstOrDefault();
                }
            }
            catch (Exception exp)
            {
                Logging.Error("Database Error", exp);
            }
            Logging.Info("Exit ProductOptionService:GetOption(Guid productId, Guid optionId)");

            return option;
        }

        public ProductOptions GetOptions(Guid productId)
        {
            Logging.Info("Entered ProductOptionService:GetOptions(Guid productId)");
            ProductOptions options = new ProductOptions();
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var list = db.ProductOptions.Where(x => x.ProductId == productId).ToList();
                    options.Items = list;
                }
            }
            catch (Exception exp)
            {
                Logging.Error("Database Error", exp);
            }
            Logging.Info("Exit ProductOptionService:GetOptions(Guid productId)");
            return options;
        }

        public void UpdateOption(Guid id, ProductOption model)
        {
            Logging.Info("Entered ProductOptionService:UpdateOption(Guid id, ProductOption model)");
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var option = db.ProductOptions.Where(x => x.Id == id).FirstOrDefault();

                    if (option != null)
                    {
                        option.Description = model.Description;
                        option.Name = model.Name;
                        option.ProductId = model.ProductId;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception exp)
            {
                Logging.Error("Database Error", exp);
            }
            Logging.Info("Exit ProductOptionService:UpdateOption(Guid id, ProductOption model)");
        }
    }
}