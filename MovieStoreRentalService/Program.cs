using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieStoreRentalService.Core;
using MovieStoreRentalService.Data;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.ModelBinders;
using MovieStoreRentalService.Services.Cart;
using MovieStoreRentalService.Services.Rentals;
using MovieStoreRentalService.Services.User;
using DateTimeModelBinderProvider = MovieStoreRentalService.ModelBinders.DateTimeModelBinderProvider;
using MovieStoreRentalService.Services;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
// Add services to the container.

//Application DB context
var connectionString = DatabaseConfiguration.ConnectionString;
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddControllersWithViews()
    .AddMvcOptions(op =>
    {
        op.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        op.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatConstants.DateFormat));
        op.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
    });

builder.Services.AddStackExchangeRedisCache(builder =>
{
    builder.Configuration ="localhost";
});

//Service injection
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
