using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> FindAll(string token);
    Task<IEnumerable<ProductViewModel>> FindAllWithCategory(string token);
    Task<ProductViewModel> FindById(int id, string token);
    Task<ProductViewModel> FindByIdWithCategory(int id, string token);
    Task<ProductViewModel> Create(ProductViewModel model, string token);
    Task<ProductViewModel> Update(ProductViewModel model, string token);
    Task<bool> DeleteById(int id, string token);
}
