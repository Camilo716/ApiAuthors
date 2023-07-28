using ApiAuthors.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuthors.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<AuthorModel>> GetAuthors()
    {
        return new List<AuthorModel>()
        {
                new AuthorModel(){Id = 1, Name = "Camilo"},
                new AuthorModel(){Id = 2, Name = "Felipe"},
        };
    }
}
