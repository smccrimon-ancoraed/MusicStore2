using System.ComponentModel.DataAnnotations;

namespace MusicStore1.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [Display(Name = "Favorite Song")]
        public string FavoriteSong { get; set; }
        public string Image { get; set; }
    }
}