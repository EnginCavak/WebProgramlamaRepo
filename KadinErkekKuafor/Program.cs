using KuaforYonetimSistemi.Data; // AppDbContext s�n�f�n� kullanabilmek i�in
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s�n� yap�land�r�n (PostgreSQL veya SQL Server kullanabilirsiniz)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // SQL Server i�in
                                                                                           // options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // PostgreSQL i�in

// Identity servisi ekleyin (kullan�c� do�rulama ve y�netimi i�in)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
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
