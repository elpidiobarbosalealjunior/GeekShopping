using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> FindAll(string token);
    Task<IEnumerable<ProductModel>> FindAllWithCategory(string token);
    Task<ProductModel> FindById(int id, string token);
    Task<ProductModel> FindByIdWithCategory(int id, string token);
    Task<ProductModel> Create(ProductModel model, string token);
    Task<ProductModel> Update(ProductModel model, string token);
    Task<bool> DeleteById(int id, string token);
}
