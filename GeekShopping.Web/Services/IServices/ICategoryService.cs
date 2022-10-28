using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

public interface ICategoryService
{
    Task<IEnumerable<CategoryModel>> FindAll(string token);
    Task<CategoryModel> FindById(int id, string token);
    Task<CategoryModel> Create(CategoryModel model, string token);
    Task<CategoryModel> Update(CategoryModel model, string token);
    Task<bool> DeleteById(int id, string token);
}
