namespace CodeFirstWebApi_Library.DTOs.BookDTOs
{
    public class BookCreateDTO:BookBaseDTO
    {
        public  string AuthorName { get; set; }
        public List<string> Genres { get; set; }
        public BookCreateDTO()
        {
            Genres=new List<string>();
        }
    }
}
