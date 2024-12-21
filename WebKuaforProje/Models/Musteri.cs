public class Musteri
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }

    // Yeni Role Alanı
    public string Role { get; set; }  // "Admin" veya "Musteri"

    public ICollection<Randevu> Randevular { get; set; }
}
