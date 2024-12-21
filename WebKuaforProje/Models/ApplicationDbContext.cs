using Microsoft.EntityFrameworkCore;
using WebKuaforProje.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Admin> Adminler { get; set; }
    public DbSet<Calisan> Clisans { get; set; }
    public DbSet<FotoOneri> FotoOneriler { get; set; }
    public DbSet<Islem> Islemler { get; set; }
    public DbSet<Musteri> Musteriler { get; set; }
    public DbSet<Price> Fiyatlar { get; set; }
    public DbSet<Randevu> Randevular { get; set; }
    public DbSet<Salon> Salonlar { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // İlişkiyi tanımlıyoruz: Bir müşteri birden fazla randevu alabilir.
        modelBuilder.Entity<Randevu>()
            .HasOne(r => r.Musteri)
            .WithMany(m => m.Randevular)
            .HasForeignKey(r => r.MusteriId);
    }
}
