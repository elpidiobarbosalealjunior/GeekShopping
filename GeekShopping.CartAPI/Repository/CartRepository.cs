using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository;

public class CartRepository : ICartRepository
{
    private readonly MySQLContext _context;
    private IMapper _mapper;

    public CartRepository(MySQLContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        var header = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
        if (header != null)
        {
            header.CouponCode = couponCode;
            _context.CartHeaders.Update(header);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> ClearCart(string userId)
    {
        var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
        if(cartHeader != null)
        {
            _context.CartDetails.RemoveRange(_context.CartDetails.Where(x => x.CartHeaderId == cartHeader.CartHeaderId));
            _context.CartHeaders.Remove(cartHeader);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<CartVO> FindByUserId(string userId)
    {
        Cart cart = new Cart
        {
            CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId),            
        };
        cart.CartDetails = _context.CartDetails.Where(x => x.CartHeaderId == cart.CartHeader.CartHeaderId).Include(x => x.Product);

        return _mapper.Map<CartVO>(cart);
    }

    public async Task<bool> RemoveCoupon(string userId)
    {
        var header = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
        if (header != null)
        {
            header.CouponCode = "";
            _context.CartHeaders.Update(header);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> RemoveFromByDetailId(int cartDetailId)
    {
        try
        {
            CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(x => x.CartDetailId == cartDetailId);
            int total = _context.CartDetails.Where(x => x.CartHeaderId == cartDetail.CartHeaderId).Count();
            _context.CartDetails.Remove(cartDetail);
            if (total == 1)
            {
                var cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(x => x.CartHeaderId == cartDetail.CartHeaderId);
                _context.CartHeaders.Remove(cartHeaderToRemove);                
            }
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<CartVO> SaveOrUpdate(CartVO cartVO)
    {
        var cart = _mapper.Map<Cart>(cartVO);

        var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == cartVO.CartDetails.FirstOrDefault().Product.CategoryId);
        if(category == null || category.CategoryId <= 0)
        {
            _context.Categories.Add(cart.CartDetails.FirstOrDefault().Product.Category ?? new Category());
            await _context.SaveChangesAsync();
        }

        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == cartVO.CartDetails.FirstOrDefault().ProductId);
        if(product == null || product.ProductId <= 0)
        {
            cart.CartDetails.FirstOrDefault().Product.Category = category;
            _context.Products.Add(cart.CartDetails.FirstOrDefault().Product ?? new Product());
            await _context.SaveChangesAsync();
        }

        var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == cart.CartHeader.UserId);
        if(cartHeader == null)
        {
            _context.CartHeaders.Add(cart.CartHeader);
            await _context.SaveChangesAsync();
            cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
            cart.CartDetails.FirstOrDefault().CartHeader = cart.CartHeader;
            cart.CartDetails.FirstOrDefault().Product = null;
            _context.CartDetails.Add(cart.CartDetails.FirstOrDefault() ?? new CartDetail() );
            await _context.SaveChangesAsync();
        }
        else
        {
            var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(x=> x.ProductId == cart.CartDetails.FirstOrDefault().ProductId && x.CartHeaderId == cartHeader.CartHeaderId);
            if(cartDetail == null)
            {
                cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                cart.CartDetails.FirstOrDefault().CartHeader = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault() ?? new CartDetail());
                await _context.SaveChangesAsync();
            }
            else
            {
                cartDetail.Count += cart.CartDetails.FirstOrDefault().Count;
                _context.CartDetails.Update(cartDetail);
                await _context.SaveChangesAsync();
            }
        }
        return _mapper.Map<CartVO>(cart);
    }
}
