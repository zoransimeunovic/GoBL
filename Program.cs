using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using GoBL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 🔹 Omogućavanje sesije
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 🔹 Lokalizacija
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("de"), new CultureInfo("sr-Latn") };
var exYuLanguages = new[] { "sr-latn", "sr-cyrl", "hr", "bs", "sr-me" };

// 🔹 Konfiguracija RequestLocalization sa sesijom i fallback logikom
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders = new[]
    {
        new CustomRequestCultureProvider(async context =>
        {
            // 1️⃣ Pokušaj sesiju
            var sessionCulture = context.Session.GetString("Culture");
            if (!string.IsNullOrEmpty(sessionCulture))
            {
                return new ProviderCultureResult(sessionCulture, sessionCulture);
            }

            // 2️⃣ Ako nema sesije → jezik sistema (Windows)
            var systemCulture = CultureInfo.CurrentCulture.Name.Split('-')[0];
            if (supportedCultures.Any(c => c.Name.StartsWith(systemCulture)))
            {
                return new ProviderCultureResult(systemCulture, systemCulture);
            }

            // 3️⃣ Ako ni sistemski nije podržan → Accept-Language header
            var acceptLang = context.Request.Headers["Accept-Language"].ToString();
            var firstLang = acceptLang.Split(',').FirstOrDefault()?.Split('-')[0];
            if (!string.IsNullOrEmpty(firstLang) && exYuLanguages.Contains(firstLang.ToLower()))
            {
                return new ProviderCultureResult("sr-Latn", "sr-Latn");
            }

            // 4️⃣ Ako ništa nije podržano → engleski
            return new ProviderCultureResult("en", "en");
        })
    };
});

builder.Services.AddHttpContextAccessor();

// 🔹 Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// 🔹 Middleware redosled
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); // ⚠ mora pre lokalizacije

var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value); // ⚡ koristi CustomRequestCultureProvider

app.UseRouting();
app.UseAuthorization();

// 🔹 Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();