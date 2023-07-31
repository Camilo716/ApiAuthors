using ApiAuthors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAuthors.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private ApplicationDbContext _context;

    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuthorModel>>> Get()
    {
       return await _context.authors.ToListAsync();
    }

    [HttpPost] 
    public async Task<ActionResult> Post(AuthorModel author)
    {
        _context.Add(author);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put([FromBody] AuthorModel author, int id)
    {
        if (author.Id != id)
            return BadRequest("Author's doesn't match with URL's id");

        _context.Update(author);
        await _context.SaveChangesAsync();

        return Ok();        
    }
}

