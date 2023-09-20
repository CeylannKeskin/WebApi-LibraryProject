using CodeFirstWebApi_Library.Entities.Abstract;

namespace CodeFirstWebApi_Library.Entities.Concrete
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }

        public Author()
        {
            AuthorBooks = new HashSet<AuthorBook>();
        }
    }
}
