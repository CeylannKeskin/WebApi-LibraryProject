using CodeFirstWebApi_Library.Entities.Abstract;

namespace CodeFirstWebApi_Library.Entities.Concrete
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
        public virtual ICollection<BookGenre> BookGenres { get; set; }
        public Book()
        {
            AuthorBooks=new HashSet<AuthorBook>();
            BookGenres=new HashSet<BookGenre>();
        }
    } 
}
