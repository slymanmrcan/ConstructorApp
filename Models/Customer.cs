using System.ComponentModel;

namespace ConstructorApp.Models
{
    public class Customer : BaseEntity
    {
        [DisplayName("Ad")]
        public string Name { get; set; }
        [DisplayName("Soyisim")]

        public string? Surname { get; set; }
        [DisplayName("Email")]

        public string Email { get; set; }
        [DisplayName("Telefon")]

        public string? Phone { get; set; }
        [DisplayName("Adres")]

        public string? Address { get; set; }
    }
}