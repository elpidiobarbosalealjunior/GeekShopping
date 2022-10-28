using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GeekShopping.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    }

    [Authorize]
    public async Task<IActionResult> ProductIndex()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var products = await _productService.FindAllWithCategory(token);
        return View(products);
    }

    public async Task<IActionResult> ProductCreate()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        ViewBag.CategoryId = new SelectList(await _categoryService.FindAll(token), "CategoryId", "Name");
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.Create(model, token);
            if (response != null) return RedirectToAction(nameof(ProductIndex));
        }
        return View(model);
    }

    public async Task<IActionResult> ProductUpdate(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        ViewBag.CategoryId = new SelectList(await _categoryService.FindAll(token), "CategoryId", "Name");
        var product = await _productService.FindById(id, token);
        if(product != null) return View(product);
        return NotFound();

    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.Update(model, token);
            if (response != null) return RedirectToAction(nameof(ProductIndex));
        }
        return View(model);
    }

    public async Task<IActionResult> ProductDelete(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        ViewBag.CategoryId = new SelectList(await _categoryService.FindAll(token), "CategoryId", "Name");
        var product = await _productService.FindById(id, token);
        if (product != null) return View(product);
        return NotFound();

    }

    [Authorize(Roles = Role.Admin)]
    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteById(model.ProductId, token);
            if (response) return RedirectToAction(nameof(ProductIndex));
        }
        return View(model);
    }
}
