using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> FindAll();
    Task<IEnumerable<ProductModel>> FindAllWithCategory();
    Task<ProductModel> FindById(int id);
    Task<ProductModel> FindByIdWithCategory(int id);
    Task<ProductModel> Create(ProductModel model);
    Task<ProductModel> Update(ProductModel model);
    Task<bool> DeleteById(int id);
}
