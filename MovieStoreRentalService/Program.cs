using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieStoreRentalService.Core;
using MovieStoreRentalService.Data;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.Data.Models;
using MovieStoreRentalService.ModelBinders;
using MovieStoreRentalService.Services.Rentals;
using DateTimeModelBinderProvider = MovieStoreRentalService.ModelBinders.DateTimeModelBinderProvider;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connectionString = DatabaseConfiguration.ConnectionString;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews()
    .AddMvcOptions(op =>
    {
        op.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        op.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(FormatConstants.DateFormat));
        op.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
    });

//Identity config
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

//Service injection
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IRentalService, RentalService>();

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
