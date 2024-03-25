using Microsoft.Extensions.DependencyInjection.Extensions;
using Nakheel_Web.Notification.Hubs;
using Nakheel_Web.Notification.MiddlewareExtensions;
using Nakheel_Web.Notification.SubscribeTableDependencies;
using System.Net.Http.Headers;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;

    //options.AddNewtonsoftJson();
});

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>(); // Add this line

//builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;//You can set Time   
});
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
});
builder.Services.AddMvcCore().AddDataAnnotations();

builder.Services.AddSignalR();

// DI
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<SubscribeNotificationTableDependency>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.Secure = CookieSecurePolicy.Always;
});

builder.Services.AddHttpClient("API", (provider, c) =>
{
    string connString = builder.Configuration.GetConnectionString("DefaultConnection");
    c.BaseAddress = new Uri(connString);
    c.DefaultRequestHeaders.Accept.Clear();
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var jwtToken = httpContextAccessor.HttpContext?.Session.GetString("JWT");

    if (!string.IsNullOrEmpty(jwtToken))
    {
        c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
    }
});

var app = builder.Build();
var connectionString = app.Configuration.GetConnectionString("NotificationConnection");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see  https://aka.ms/aspnetcore-hsts .
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.UseIpRateLimiting();


app.MapHub<NotificationHub>("/notificationHub");
app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("Server");
    context.Response.Headers.Remove("X-Powered-By");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    //context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';script-src 'self';style-src 'self' 'unsafe-inline' https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css ;");
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    //var isLoggedIn = context.Session.GetString("LoginStatus");
    //if (!string.IsNullOrEmpty(isLoggedIn) && isLoggedIn == "true")
    //{
    //    app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);
    //    var key1 = "LoginStatus";
    //    var str1 = "false"; 
    //    context.Session.SetString(key1, str1);
    //}
    await next();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

//app.UseExceptionHandler("/Home/Error");

//app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//                Path.Combine(Directory.GetCurrentDirectory(), "ErrorFile")),
//    RequestPath = "/ErrorFile"
//});
app.UseCookiePolicy();

app.Run();