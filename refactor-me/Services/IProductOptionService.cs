using refactor_me.Models;
using System;

namespace refactor_me.Services
{
    interface IProductOptionService
    {
        ProductOption GetOption(Guid productId, Guid optionId);
        ProductOptions GetOptions(Guid productId);
        void CreateOption(Guid productId, ProductOption option);
        void UpdateOption(Guid id, ProductOption option);
        void DeleteProduct(Guid optionId);
    }
}
