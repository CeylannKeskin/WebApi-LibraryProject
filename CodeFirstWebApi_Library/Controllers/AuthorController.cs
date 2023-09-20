using CodeFirstWebApi_Library.DTOs.AuthorDTOs;
using CodeFirstWebApi_Library.Entities.Concrete;
using CodeFirstWebApi_Library.Entities.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstWebApi_Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly LibraryDbContext _db;

        public AuthorController(LibraryDbContext db)
        {
            _db = db;
        }
        [HttpGet] //bütün liste(authors)
        [Route("GetAllAuthors")]
        public ActionResult<IEnumerable<AuthorDTO>> GetAll()
        {
            List<AuthorDTO> authors = _db.Authors.Select(x =>
                new AuthorDTO()
                {
                    ID = x.ID,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToList();
            if (authors is null) return NotFound();
            return authors;
        }

        //id ile ara 
        [HttpGet]
        [Route("AuthorDetail")]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthor(int id)
        {
            Author author = _db.Authors.FirstOrDefault(x => x.ID == id);

            if (author is null) return NotFound();

            AuthorDTO authorDto = new AuthorDTO()
                {
                    ID = author.ID,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                };
            if (authorDto is null) return NotFound();
            return Ok(authorDto);
        }

        //ekleme 
        [HttpPost]
        [Route("CreateAuthor")]
        public ActionResult<IEnumerable<AuthorCreateDTO>> CreateAuthor(AuthorCreateDTO authorDTO)
        {
            try
            {
                Author author = new Author()
                {
                    FirstName = authorDTO.FirstName,
                    LastName = authorDTO.LastName
                };

                _db.Authors.Add(author);
                _db.SaveChanges();
                return Ok(authorDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            
        }

        //güncelleme 
        [HttpPut]
        [Route("UpdateAuthor")]
        public ActionResult<AuthorDTO> UpdateAuthor(AuthorDTO authorDTO)
        {
            Author author = _db.Authors.FirstOrDefault(x => x.ID == authorDTO.ID);
            if (author == null) return NotFound();
            try
            {
                author.FirstName = authorDTO.FirstName;
                author.LastName = authorDTO.LastName;
                _db.Entry<Author>(author).State = EntityState.Modified;
                _db.SaveChanges();
                return Ok(authorDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //sil
        [HttpDelete]
        [Route("DeleteAuthor")]
        public IActionResult DeleteAuthor(int id)
        {
            Author author = _db.Authors.Find(id);
            if (author == null) return NotFound();

            try
            {
                _db.Authors.Remove(author);
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
