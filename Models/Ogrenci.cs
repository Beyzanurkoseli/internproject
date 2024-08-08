namespace UniversiteOgrenciYonetimSistemi.Models
{
    public class Ogrenci
    {
        public int Id { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Universite { get; set; }
        public string? Bolum { get; set; }
        public int? Sinif { get; set; }
        public string? Email { get; set; }
        public string? SifreHash { get; set; }
        public string? SifreSalt { get; set; }
    }
}
