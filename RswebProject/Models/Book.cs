using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Linq;

namespace RswebProject.Models
{
    public class Book
    {

        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Range(1, 10)]
        public decimal? Rating { get; set; }
        
        [Display(Name = "Author")]
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }

        public ICollection<BookPHouse>? PHouses { get; set; }
    }
}
