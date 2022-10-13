using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices;

public interface ICategoryService
{
    Task<IEnumerable<CategoryModel>> FindAll();
    Task<CategoryModel> FindById(int id);
    Task<CategoryModel> Create(CategoryModel model);
    Task<CategoryModel> Update(CategoryModel model);
    Task<bool> DeleteById(int id);
}
