﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RswebProject.Models
{
    public class Author
    {

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [NotMapped]
        public int Age
        {
            get
            {
                TimeSpan span = DateTime.Now - BirthDate;
                double years = (double)span.TotalDays / 365.2425;
                return (int)years;
            }
        }

        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        public ICollection<Book>? Books { get; set; }
    }
}
