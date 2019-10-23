using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore1.Models
{
    public class Song
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }


    }
}
