using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    public const string BasePath = "api/v1/product";

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<IEnumerable<ProductModel>> FindAll()
    {
        var response = await _httpClient.GetAsync(BasePath);
        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<IEnumerable<ProductModel>> FindAllWithCategory()
    {
        var response = await _httpClient.GetAsync($"{BasePath}/category");
        return await response.ReadContentAs<List<ProductModel>>();
    }

    public async Task<ProductModel> FindById(int id)
    {
        var response = await _httpClient.GetAsync($"{BasePath}/{id}");
        return await response.ReadContentAs<ProductModel>();
    }

    public Task<ProductModel> FindByIdWithCategory(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductModel> Create(ProductModel model)
    {
        var response = await _httpClient.PostAsJson(BasePath, model);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<ProductModel>();
        else
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<ProductModel> Update(ProductModel model)
    {
        var response = await _httpClient.PutAsJson(BasePath, model);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<ProductModel>();
        else
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> DeleteById(int id)
    {
        var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<bool>();
        else
            throw new Exception("Something went wrong when calling API");
    }
}
