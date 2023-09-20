using CodeFirstWebApi_Library.Entities.Abstract;

namespace CodeFirstWebApi_Library.Entities.Concrete
{
    public class AuthorBook : BaseEntity
    {
        public int? AuthorID { get; set; }
        public virtual Author? Author { get; set; }
        public int? BookID { get; set; }
        public virtual Book Book { get; set; }
    }
}
