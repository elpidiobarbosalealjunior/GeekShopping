﻿using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services;

public class CouponService : ICouponService
{
    private readonly HttpClient _httpClient;
    public const string BasePath = "api/v1/coupon";

    public CouponService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<CouponViewModel> GetByCouponCode(string couponCode, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BasePath}/{couponCode}");
        return await response.ReadContentAs<CouponViewModel>();
    }
}
