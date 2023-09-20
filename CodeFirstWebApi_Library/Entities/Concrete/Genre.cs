using CodeFirstWebApi_Library.Entities.Abstract;

namespace CodeFirstWebApi_Library.Entities.Concrete
{
    public class Genre:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
        public Genre()
        {
            BookGenres=new HashSet<BookGenre>();
        }
    }
}
