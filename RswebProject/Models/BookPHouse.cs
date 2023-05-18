namespace RswebProject.Models
{
    public class BookPHouse
    {
        public int Id { get; set; }
        public int PHouseId { get; set; }

        public PHouse? PHouse { get; set; }
        public int BookId { get; set; }

        public Book? Book { get; set; }
    
}
}
