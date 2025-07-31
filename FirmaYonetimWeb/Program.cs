using Microsoft.EntityFrameworkCore;
using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Repositories;
using Microsoft.AspNetCore.Identity;
using FirmaYonetimWeb.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("tr-TR");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var culture = new CultureInfo("tr-TR");
    options.DefaultRequestCulture = new RequestCulture(culture);
    options.SupportedCultures = [culture];
    options.SupportedUICultures = [culture];
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options =>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;             // �ifre en az 1 rakam i�ermeli
    options.Password.RequiredLength = 3;               // �ifre en az 8 karakter uzunlu�unda olmal�
    options.Password.RequireUppercase = false;          // �ifre en az 1 b�y�k harf i�ermeli
    options.Password.RequireLowercase = false;          // �ifre en az 1 k���k harf i�ermeli
    options.Password.RequireNonAlphanumeric = false;   // �ifre �zel karakter (!, @, #, vs.) zorunlu mu?
    options.Lockout.MaxFailedAccessAttempts = 5; // 5 ba�ar�s�z giri� denemesi sonras� hesap kilitlensin
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.HttpOnly = true;
    opt.Cookie.Name = "IdentityCookie";
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.LoginPath = new PathString("/Account/SignIn");
});


builder.Services.AddTransient<IBelediyeRepository, EfBelediyeRepository>();
builder.Services.AddTransient<IKaynakTuruRepository, EfKaynakTuruRepository>();
builder.Services.AddTransient<IVPNTuruRepository, EfVPNAltTuruRepository>();
builder.Services.AddTransient<IRDPRepository, EfRDPRepository>();
builder.Services.AddTransient<IVPNRepository, EfVPNRepository>();
builder.Services.AddTransient<IServiceRepository, EfServiceRepository>();
builder.Services.AddTransient<IBelediyeKaynakRepository,EfBelediyeKaynakRepository>();
builder.Services.AddTransient<IKaynakGirisRepository, EfKaynakGirisRepository>();
builder.Services.AddTransient<IPostrgreRepository, EfPostrgeRepository>();
builder.Services.AddTransient<IAnyRepository, EfAnyRepository>();
builder.Services.AddTransient<INotRepository, EfNotRepository>();
builder.Services.AddTransient<IGeoServerRepository, EfGeoServerRepository>();
builder.Services.AddScoped<IBelediyePersonelRepository, EfBelediyePersonelRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}")
    .WithStaticAssets();

app.Run();
