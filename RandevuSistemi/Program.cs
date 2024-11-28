using Microsoft.EntityFrameworkCore;
using RandevuSistemi.Data;
using NLog;
using NLog.Web;
using RandevuSistemi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

// Internal log file path based on the operating system
string internalLogFilePath;
if (Environment.OSVersion.Platform == PlatformID.Unix)
{
    internalLogFilePath = "/tmp/RandevuInternal.txt";
}
else
{
    internalLogFilePath = "c:/temp/RandevuInternal.txt";
}
// Set environment variable for internal log file path
Environment.SetEnvironmentVariable("internalLogFilePath", internalLogFilePath);

// NLog configuration file path
string nlogConfigPath = "nlog.config";
var logger = LogManager.Setup()
    .LoadConfigurationFromFile(nlogConfigPath)
    .GetCurrentClassLogger();



// Add services to the container.
builder.Services.AddDbContext<RandevuDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));
builder.Services.AddScoped<RandevuDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // GDPR uyumluluğu için
});

    // CustomExceptionLogger'ı DI konteynerine ekleyin
        builder.Services.AddSingleton<CustomExceptionLogger>();
    var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=GirisYap}/{action=Index}/{id?}");

app.Run();
