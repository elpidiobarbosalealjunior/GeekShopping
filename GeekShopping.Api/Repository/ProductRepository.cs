using AutoMapper;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Model;
using GeekShopping.ProductApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductApi.Repository;

public class ProductRepository : IProductRepository
{
    private readonly MySQLContext _context;
    private IMapper _mapper;

    public ProductRepository(MySQLContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductVO>> FindAll()
    {
        List<Product> products = await _context.Products.ToListAsync();
        return _mapper.Map<List<ProductVO>>(products);
    }

    public async Task<IEnumerable<ProductVO>> FindAllWithCategory()
    {
        List<Product> products = await _context.Products.Include(p => p.Category).ToListAsync();
        return _mapper.Map<List<ProductVO>>(products);
    }

    public async Task<ProductVO> FindById(int id)
    {
        Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id) ?? new Product();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> FindByIdWithCategory(int id)
    {
        Product product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id) ?? new Product();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> Create(ProductVO productVO)
    {
        Product product = _mapper.Map<Product>(productVO);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> Update(ProductVO productVO)
    {
        Product product = _mapper.Map<Product>(productVO);
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id) ?? new Product() ;
            if (product.ProductId <= 0)
                return false;
            else
            {
                _context.Products.Remove(product);
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
