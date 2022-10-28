using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    public const string BasePath = "api/v1/category";

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<IEnumerable<CategoryModel>> FindAll(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync(BasePath);
        return await response.ReadContentAs<List<CategoryModel>>();
    }

    public async Task<CategoryModel> FindById(int id, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BasePath}/{id}");
        return await response.ReadContentAs<CategoryModel>();
    }

    public async Task<CategoryModel> Create(CategoryModel model, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PostAsJson(BasePath, model);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<CategoryModel>();
        else
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<CategoryModel> Update(CategoryModel model, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PutAsJson(BasePath, model);
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<CategoryModel>();
        else
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<bool> DeleteById(int id, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
        if (response.IsSuccessStatusCode)
            return await response.ReadContentAs<bool>();
        else
            throw new Exception("Something went wrong when calling API");
    }
}