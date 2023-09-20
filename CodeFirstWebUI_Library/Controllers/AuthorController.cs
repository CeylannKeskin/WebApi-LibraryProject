using CodeFirstWebUI_Library.Models.AuthorVMs;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstWebUI_Library.Controllers
{
    public class AuthorController : Controller
    {
        private readonly string baseAdress = "https://localhost:7124";
        public IActionResult Index()
        {
            List<AuthorVM> authorVMs;

            using(var client=new HttpClient())
            {
                client.BaseAddress=new Uri(baseAdress);
                var response = client.GetAsync("Author/GetAllAuthors");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadFromJsonAsync<List<AuthorVM>>();
                    read.Wait();
                    authorVMs = read.Result;
                    return View(authorVMs);
                }
                else
                    return NotFound();

            }



            return View();
        }
    }
}
