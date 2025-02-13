namespace ConstructorApp.Models
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }    // Toplam sayfa sayısı
        public string PageUrl { get; set; }    // Sayfa URL'si (Link formatı)
    }
}