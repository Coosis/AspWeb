using Microsoft.AspNetCore.HttpOverrides;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddAuthentication("HHUCookieScheme")
    .AddCookie("HHUCookieScheme", options =>
    {
        options.Cookie.Name = "HHUCookie";
        options.LoginPath = "/Login";
        //options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "HHUCookie";
    options.ExpireTimeSpan = TimeSpan.FromDays(365 * 100);  // Expires in 100 years
    options.SlidingExpiration = false;  // Disable sliding expiration
});


builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHttpsRedirection();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
