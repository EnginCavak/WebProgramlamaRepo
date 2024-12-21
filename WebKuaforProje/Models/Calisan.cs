using WebKuaforProje.Models;

public class Calisan
{
    public int CalisanID { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public ICollection<Randevu> Randevular { get; set; }
}
