using CodeFirstWebApi_Library.DTOs.BookDTOs;
using CodeFirstWebApi_Library.Entities.Concrete;
using CodeFirstWebApi_Library.Entities.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstWebApi_Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext _db;

        public BookController(LibraryDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public ActionResult<IEnumerable<BookDTO>> GetAllBook()
        {
            List<BookDTO> bookDTOs = _db.Books.Select(x => new BookDTO
            {
                ID = x.ID,
                Name = x.Name,

            }).ToList();
            return Ok(bookDTOs);
        }
        [HttpPost]
        [Route("CreateBook")]
        public ActionResult<BookCreateDTO> CreateBook(BookCreateDTO bookCreateDTO)
        {
            if (bookCreateDTO == null) return BadRequest();
            try
            {
                Author author = _db.Authors.FirstOrDefault(x => x.FirstName.ToLower().Equals(bookCreateDTO.AuthorName.ToLower()));

                Book book = new Book()
                {
                    Name = bookCreateDTO.Name,
                };
                ExistOrCreateBookGenres(bookCreateDTO, book);
                string msg = ExistAuthor(author, book);
                if (msg != null) return NotFound(msg);
                _db.Books.Add(book);
                _db.SaveChanges();
                return CreatedAtAction("GetBook", new {id=book.ID},book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        private string ExistAuthor(Author? author, Book book)
        {
            if (author == null)
            {
                return "Önce yazar bilgilerini Girmelisniz";
            }
            AuthorBook authorBook = new AuthorBook()
            {
                Author = author,
                Book = book
            };
            author.AuthorBooks.Add(authorBook);
            book.AuthorBooks.Add(authorBook);
            _db.AuthorBooks.Add(authorBook);
            return null;
        }

        private void ExistOrCreateBookGenres(BookCreateDTO bookCreateDTO, Book book)
        {
            List<Genre> genres = _db.Genres.Where(x => bookCreateDTO.Genres.Contains(x.Name)).ToList();

            List<string> genreName = _db.Genres.Select(x => x.Name).ToList();
            List<string> names = bookCreateDTO.Genres.Where(x => !genreName.Contains(x)).ToList();

            foreach (var item in names)
            {
                Genre genre = new Genre()
                {
                    Name = item
                };
                genres.Add(genre);
                _db.Genres.Add(genre);
            }
            foreach (var genre in genres)
            {
                BookGenre bookGenre = new BookGenre()
                {

                    Book = book,
                    Genre = genre
                };
                book.BookGenres.Add(bookGenre);
                genre.BookGenres.Add(bookGenre);
                _db.BookGenres.Add(bookGenre);
            }
        }

        //kitap detayı
        [HttpGet]
        [Route("BookDetail")]
        public ActionResult<BookDTO> GetBook(int id)
        {
            Book book = _db.Books.FirstOrDefault(x => x.ID == id);
            if (book == null) return NotFound();
            BookDTO bookDTO = new BookDTO()
            {
                ID = id,
                Name = book.Name
            };
            AuthorBook authorBook = _db.AuthorBooks.Where(x => x.BookID == book.ID).FirstOrDefault();
            bookDTO.Author = _db.Authors.FirstOrDefault(x => x.ID == authorBook.AuthorID).FirstName;
            return Ok(bookDTO);
        }

        //güncelleme
        [HttpPut]
        [Route("UpdateBook")]
        public ActionResult<BookDTO> UpdateBook(BookDTO bookDto)
        {
            Book book = _db.Books.FirstOrDefault(x => x.ID.Equals(bookDto.ID));
            if (book == null) { return NotFound(); }
            try
            {
                book.Name = bookDto.Name;

                Author author = _db.Authors.FirstOrDefault(x => x.FirstName == bookDto.Author);

                if (author == null) { return BadRequest(); }
                else
                {
                    AuthorBook authorBook = _db.AuthorBooks.FirstOrDefault(x => x.BookID == book.ID);

                    authorBook.Book = book;
                    authorBook.Author = author;

                    _db.SaveChanges();
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            Book book = _db.Books.FirstOrDefault(x => x.ID == id);
            AuthorBook authorBook = _db.AuthorBooks.FirstOrDefault(x => x.BookID == book.ID);
            BookGenre bookGenre = _db.BookGenres.FirstOrDefault(x => x.BookID == book.ID);

            if (book == null) return NotFound(id);
            try
            {
                _db.Books.Remove(book);
                _db.AuthorBooks.Remove(authorBook);
                _db.BookGenres.Remove(bookGenre);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
