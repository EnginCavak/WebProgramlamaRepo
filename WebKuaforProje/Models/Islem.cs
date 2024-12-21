public class Islem
{
    public int IslemID { get; set; }
    public string IslemAdi { get; set; }
    public decimal Fiyat { get; set; }
    public string Aciklama { get; set; }

    public int PriceId { get; set; }
    public Price Price { get; set; }
}
