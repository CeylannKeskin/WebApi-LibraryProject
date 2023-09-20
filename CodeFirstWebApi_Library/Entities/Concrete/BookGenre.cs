using CodeFirstWebApi_Library.Entities.Abstract;

namespace CodeFirstWebApi_Library.Entities.Concrete
{
    public class BookGenre:BaseEntity
    {
        public int? BookID { get; set; }
        public virtual Book? Book { get; set; }
        public int? GenreID { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
