using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> FindAll(string token);
    Task<CategoryViewModel> FindById(int id, string token);
    Task<CategoryViewModel> Create(CategoryViewModel model, string token);
    Task<CategoryViewModel> Update(CategoryViewModel model, string token);
    Task<bool> DeleteById(int id, string token);
}
