using Core.Services;
using Core.Services.Interfaces;
using DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using WebMarkupMin.AspNetCore6;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddWebMarkupMin(
    options =>
    {
        options.AllowMinificationInDevelopmentEnvironment = true;
        options.AllowCompressionInDevelopmentEnvironment = true;
    })
    .AddHtmlMinification(
        options =>
        {
            options.MinificationSettings.RemoveRedundantAttributes = true;
            options.MinificationSettings.RemoveHttpProtocolFromAttributes = true;
            options.MinificationSettings.RemoveHttpsProtocolFromAttributes = true;
        })
    .AddHttpCompression();

#region DatabaseContext
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<MyContext>(options =>
options.UseSqlServer("name=ConnectionStrings:HoozadConnection"));
#endregion DatabaseContext
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.AccessDeniedPath = "/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);

});
#region IOC
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISuppService, SuppService>();
//builder.Services.AddScoped<IPageBannerService, PageBannerService>();
builder.Services.AddScoped<IStoreService, StoreService>();
#endregion IOC
#region tempkey
var keysFolder = Path.Combine(builder.Environment.ContentRootPath, "wwwroot/temp-keys");
builder.Services.AddDataProtection()
.PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
.SetDefaultKeyLifetime(TimeSpan.FromDays(180));
#endregion
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();

builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseWebMarkupMin();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.MapRazorPages();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "AdminArea",
    areaName: "UsersPanel",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();


app.Run();
