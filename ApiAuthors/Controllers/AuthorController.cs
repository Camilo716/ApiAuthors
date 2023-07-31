using ApiAuthors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAuthors.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private ApplicationDbContext _context;

    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuthorModel>>> GetAuthors()
    {
       return await _context.authors.ToListAsync();
    }

    [HttpPost] 
    public async Task<ActionResult> PostAuthors(AuthorModel author)
    {
        _context.Add(author);
        await _context.SaveChangesAsync();
        return Ok();
    }
}

