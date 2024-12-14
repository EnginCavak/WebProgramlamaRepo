using KuaforYonetimSistemi.Data; // ApplicationDbContext s�n�f�n� kullanabilmek i�in
//using KuaforYonetimSistemi.Services; // IMusteriService ve MusteriService i�in
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL veritaban� ba�lant�s�n� yap�land�r�n
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // PostgreSQL ba�lant�s�

// IMusteriService ve MusteriService'i DI container'a ekleyin
builder.Services.AddScoped<IMusteriService, MusteriService>();

// Identity servisi ekleyin (kullan�c� do�rulama ve y�netimi i�in)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// MVC'yi yap�land�r�n
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Geli�tirme ortam�nda hata ayr�nt�lar�n� g�ster
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Hata sayfas�, �retim ortam�nda kullan�lacak
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Statik dosyalar (CSS, JS, resimler vb.) i�in middleware
app.UseStaticFiles();

// HTTP talep y�nlendirmesi
app.UseRouting();

// Kullan�c� kimlik do�rulama (Authentication) middleware
app.UseAuthentication();

// Yetkilendirme middleware (yetki denetimi i�in)
app.UseAuthorization();

// Uygulama rota y�nlendirmeleri
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulamay� �al��t�r�n
app.Run();
