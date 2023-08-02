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
       return await _context.authors.Include(a => a.books).ToListAsync();
    }

    [HttpGet("first")]
    public async Task<ActionResult<AuthorModel>> GetFirstAuthor ()
    {   
        return await _context.authors.Include(a => a.books).FirstOrDefaultAsync();
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

        bool exist = await _context.authors.AnyAsync(a => a.Id == id);
        if (!exist)
        {
            return NotFound();
        } 

        _context.Update(author);
        await _context.SaveChangesAsync();
        return Ok();        
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool exist = await _context.authors.AnyAsync(a => a.Id == id);
        if (!exist)
        {
            return NotFound();
        } 

        AuthorModel author = _context.authors.Find(id)!;
        _context.Remove(author);
        await _context.SaveChangesAsync();
        return Ok();
    }
}

