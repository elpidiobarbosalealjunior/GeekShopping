using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
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

    public async Task<IActionResult> ProductIndex()
    {
        var products = await _productService.FindAllWithCategory();
        return View(products);
    }

    public async Task<IActionResult> ProductCreate()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.FindAll(), "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.Create(model);
            if (response != null) return RedirectToAction(nameof(ProductIndex));
        }
        return View(model);
    }

    public async Task<IActionResult> ProductUpdate(int id)
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.FindAll(), "CategoryId", "Name");
        var product = await _productService.FindById(id);
        if(product != null) return View(product);
        return NotFound();

    }

    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.Update(model);
            if (response != null) return RedirectToAction(nameof(ProductIndex));
        }
        return View(model);
    }

    public async Task<IActionResult> ProductDelete(int id)
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.FindAll(), "CategoryId", "Name");
        var product = await _productService.FindById(id);
        if (product != null) return View(product);
        return NotFound();

    }

    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.DeleteById(model.ProductId);
            if (response) return RedirectToAction(nameof(ProductIndex));
        }
        return View(model);
    }
}
