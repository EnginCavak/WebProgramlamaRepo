using System.ComponentModel.DataAnnotations;

namespace WebKuaforProje.Models
{
    public class Randevu
    {
        public int Id { get; set; }

        [Required]
        public DateTime RandevuTarihi { get; set; }

        [Required]
        [StringLength(100)]
        public string Hizmet { get; set; }

        public string KullaniciAdi { get; set; }

        public int MusteriId { get; set; }  // Yabancı anahtar
        public Musteri Musteri { get; set; }  // Müşteri nesnesi ile ilişki
    }
}
