using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services;

public class CartService : ICartService
{
    private readonly HttpClient _httpClient;
    public const string BasePath = "api/v1/cart";

    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<CartViewModel> FindByUserId(string userId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BasePath}/find-cart/{userId}");
        return await response.ReadContentAs<CartViewModel>();
    }

    public async Task<CartViewModel> AddItem(CartViewModel cart, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PostAsJson($"{BasePath}/add-cart", cart);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<CartViewModel>();
        else
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<CartViewModel> Update(CartViewModel cart, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PutAsJson($"{BasePath}/update-cart", cart);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<CartViewModel>();
        else
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> ApplyCoupon(CartHeaderViewModel header, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PostAsJson($"{BasePath}/apply-coupon", header);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<bool>();
        else
            throw new Exception("Something went wrong when calling API");
    }

    public Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string token)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Clear(string userId, string token)
    {
        throw new NotImplementedException();
    }


    public async Task<bool> RemoveCoupon(string userId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"{BasePath}/remove-coupon/{userId}");
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<bool>();
        else
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> RemoveFrom(int cartId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"{BasePath}/remove-cart/{cartId}");
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<bool>();
        else
            throw new Exception("Something went wrong when calling API");
    }
}
