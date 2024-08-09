using System.ComponentModel.DataAnnotations;

namespace UniversiteOgrenciYonetimSistemi.Models
{
    // Öğrenci sınıfı, kullanıcı bilgilerini temsil eder
    public class Ogrenci
    {
        // Öğrencinin benzersiz kimliği
        public int Id { get; set; }

        // Ad alanı zorunludur
        [Required(ErrorMessage = "Ad gerekli.")]
        public string? Ad { get; set; }

        // Soyad alanı zorunludur
        [Required(ErrorMessage = "Soyad gerekli.")]
        public string? Soyad { get; set; }

        // Email alanı zorunludur ve geçerli bir email formatında olmalıdır
        [Required(ErrorMessage = "Email gerekli.")]
        [EmailAddress(ErrorMessage = "Geçersiz email formatı.")]
        public string? Email { get; set; }

        // Şifre alanı zorunludur ve en az 6 karakter uzunluğunda olmalıdır
        [Required(ErrorMessage = "Şifre gerekli.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string? Sifre { get; set; }

        // Üniversite bilgisi (zorunlu değil)
        public string? Universite { get; set; }

        // Bölüm bilgisi (zorunlu değil)
        public string? Bolum { get; set; }

        // Sınıf bilgisi (zorunlu değil)
        public int Sinif { get; set; }
    }
}

