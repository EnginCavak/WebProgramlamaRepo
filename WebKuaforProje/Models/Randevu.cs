using System.ComponentModel.DataAnnotations;

public class Randevu
{
    public int RandevuID { get; set; }

    [Required]
    public DateTime RandevuTarihi { get; set; }

    [Required]
    [StringLength(100)]
    public string Hizmet { get; set; }

    public string KullaniciAdi { get; set; }

    public int MusteriID { get; set; }
    public Musteri Musteri { get; set; }

    public int CalisanID { get; set; }
    public Calisan Calisan { get; set; }
}
