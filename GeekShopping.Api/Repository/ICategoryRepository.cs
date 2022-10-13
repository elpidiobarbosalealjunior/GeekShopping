using GeekShopping.Api.Data.ValueObjects;

namespace GeekShopping.Api.Repository
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
