using GeekShopping.ProductApi.Data.ValueObjects;

namespace GeekShopping.ProductApi.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryVO>> FindAll();
        Task<CategoryVO> FindById(int id);
        Task<CategoryVO> Create(CategoryVO productVO);
        Task<CategoryVO> Update(CategoryVO productVO);
        Task<bool> Delete(int id);
    }
}
