using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;
using System.Net;

namespace GeekShopping.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IdentityModelEventSource.ShowPII = true;
           
            // Add services to the container.
            builder.Services.AddHttpClient<IProductService, ProductService>(c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:ProductAPI"]));
            builder.Services.AddHttpClient<ICategoryService, CategoryService>(c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:ProductAPI"]));
            builder.Services.AddHttpClient<ICartService, CartService>(c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:CartApi"]));
            builder.Services.AddHttpClient<ICouponService, CouponService>(c => c.BaseAddress = new Uri(builder.Configuration["ServicesUrls:CouponApi"]));

            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            })
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = builder.Configuration["ServicesUrls:IdentityServer"];
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClientId = "gks.web";
                options.ClientSecret = "0cdoci5f";
                options.RequireHttpsMetadata = false;
                options.Scope.Add("profile");
                options.Scope.Add("openid");
                options.Scope.Add("geek_shopping.read");
                options.Scope.Add("geek_shopping.write");
                options.Scope.Add("geek_shopping.delete");
                options.ResponseType = "code";
                options.UsePkce = true;
                options.ClaimActions.MapJsonKey("role", "role", "role");
                options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "role";
                options.SaveTokens = true;
            });
           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();            

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}