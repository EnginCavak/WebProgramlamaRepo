using KuaforYonetimSistemi.Data; // ApplicationDbContext'i kullanabilmek i�in
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL ba�lant�s�n� yap�land�r�n
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("VeriTabaniWebKuaforConnection")));  // PostgreSQL ba�lant�s� i�in

builder.Services.AddControllersWithViews(); // MVC yap�land�rmas�

var app = builder.Build();

// Geli�tirme ortam�nda hata ayr�nt�lar�n� g�ster
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
