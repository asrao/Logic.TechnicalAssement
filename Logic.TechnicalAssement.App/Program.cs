using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
options.IdleTimeout = TimeSpan.FromMinutes(60);// This indicates how long the session can be idle before its contents are abandoned. 
    options.IOTimeout = TimeSpan.FromSeconds(10);
options.Cookie.Name = ".Logic.TechnicalAssessment.Session";
options.Cookie.HttpOnly = true;
options.Cookie.IsEssential = true;
options.Cookie.Path = "/";
options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
     app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Leave}/{action=Index}/{id?}");
app.UseSession();
app.Run();
