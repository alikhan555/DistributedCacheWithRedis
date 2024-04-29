using DistributedCacheWithRedis.Service.DistributedCache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurations of Distributed Cache with Redis
builder.Services.AddSingleton<IDistributedCacheService, DistributedCacheService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("DistributedCache:Redis:ConnectionString");
    options.InstanceName = builder.Configuration.GetValue<string>("DistributedCache:Redis:InstanceName");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
