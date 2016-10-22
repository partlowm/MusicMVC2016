using System.ComponentModel.DataAnnotations;

namespace MusicFall2016.Models
{
    public class Genre
    {
        
        public int GenreID { get; set; }
        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(20, ErrorMessage = "Can only be 20 Chars")]
        public string Name { get; set; }
    }
}