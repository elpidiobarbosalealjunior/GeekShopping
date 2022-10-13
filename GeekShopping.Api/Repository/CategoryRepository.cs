using AutoMapper;
using GeekShopping.Api.Data.ValueObjects;
using GeekShopping.Api.Model;
using GeekShopping.Api.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CategoryRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryVO>> FindAll()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryVO>>(categories);
        }

        public async Task<CategoryVO> FindById(int id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryId == id) ?? new Category();
            return _mapper.Map<CategoryVO>(category);
        }

        public async Task<CategoryVO> Create(CategoryVO categoryVO)
        {
            Category category = _mapper.Map<Category>(categoryVO);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryVO>(category);
        }

        public async Task<CategoryVO> Update(CategoryVO categoryVO)
        {
            Category category = _mapper.Map<Category>(categoryVO);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryVO>(category);
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(p => p.CategoryId == id) ?? new Category() ;
                if (category.CategoryId <= 0)
                    return false;
                else
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        } 
    }
}
