using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RswebProject.Models
{
    public class PHouse
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]

        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "City")]
        public string LastName { get; set; }

        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        public ICollection<BookPHouse>? Books { get; set; }
    }
}
