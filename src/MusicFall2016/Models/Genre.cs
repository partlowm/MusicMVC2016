using System.ComponentModel.DataAnnotations;

namespace MusicFall2016.Models
{
    public class Genre
    {
        
        public int GenreID { get; set; }
        [Required(ErrorMessage = "Genre is required.")]
        public string Name { get; set; }
    }
}