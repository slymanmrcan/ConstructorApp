using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConstructorApp.Models
{
    public class Project:BaseEntity
    {
        [DisplayName("Proje Adı")]
        public string Name { get; set; }
        [DisplayName("Açıklama")]

        public string Description { get; set; }
        [DisplayName("Fiyat")]

        public double Prize { get; set; }  
        [DisplayName("Lokasyon")]

        public string Location { get; set; }
    }
}