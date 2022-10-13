using GeekShopping.Api.Data.ValueObjects;

namespace GeekShopping.Api.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAll();
        Task<IEnumerable<ProductVO>> FindAllWithCategory();
        Task<ProductVO> FindById(int id);
        Task<ProductVO> FindByIdWithCategory(int id);
        Task<ProductVO> Create(ProductVO productVO);
        Task<ProductVO> Update(ProductVO productVO);
        Task<bool> Delete(int id);
    }
}
